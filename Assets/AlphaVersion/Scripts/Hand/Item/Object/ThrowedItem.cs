using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    public enum ItemType
    {
        Scotch,  // Glass01
        Bourbon, // Glass02
        Cognac,  // Glass03
        Potato,  // Potato01
        Beef,    // RoastBeef
        MiniActor, // 末尾にあることで判定するのでｺｺ
    }

    /// <summary>
    /// 投げるアイテム全てが共通して持つコンポーネント
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowedItem : MonoBehaviour, ICatchable
    {
        [SerializeField] ParticleSystem _trail;
        [SerializeField] GameObject _decal;

        GameObject _decapu;
        ThrowedItemPool _pool; // プール
        ItemSettingsSO _settings;
        Rigidbody _rigidbody;
        Vector3 _startingPoint;
        RigidbodyConstraints _defaultConstraints;
        public bool IsThrowed { get; private set; }

        public float Height => _settings.Height;

        /// <summary>
        /// 水平に移動した距離の2乗を返す
        /// </summary>
        public float MovingSqrDistance
        {
            get
            {
                Vector3 current = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 start = new Vector3(_startingPoint.x, 0, _startingPoint.z);
                return (current - start).sqrMagnitude;
            }
        }

        public ItemType Type => _settings.Type;
        public float SqrSpeed => _rigidbody.velocity.sqrMagnitude;

        /// <summary>
        /// 生成してプールに追加した際に1度だけプール側から呼び出されるメソッド
        /// </summary>
        public void OnCreate(ThrowedItemPool pool)
        {
            if (_decal != null)
            {
                // デカールを使いまわす
                _decapu = Instantiate(_decal);
                _decapu.SetActive(false);
            }

            _pool = pool;
            _rigidbody = GetComponent<Rigidbody>();
            _defaultConstraints = _rigidbody.constraints;

            // ゲームオーバー時にトークンをDisposeする
            MessageBroker.Default.Receive<GameOverMessage>()
                .Subscribe(_ => OnGameOver()).AddTo(gameObject);
        }

        /// <summary>
        /// 外部からプールから取り出した際に初期化する、Awakeの代用メソッド
        /// </summary>
        public void Init(ItemSettingsSO settings)
        {
            IsThrowed = false;      
            _settings = settings;

            _trail.Stop();
            Stop();
            FreezeXZ();
        }

        /// <summary>
        /// ゲームオーバーになった際に呼ばれる。
        /// </summary>
        void OnGameOver()
        {
            Stop(isKinematic: true);
        }

        /// <summary>
        /// 崩れないようにXとZ方向への移動を制限する
        /// </summary>
        void FreezeXZ()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
                                     RigidbodyConstraints.FreezePositionZ;
        }

        /// <summary>
        /// 指定した方向/威力で投げる
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            _rigidbody.constraints = _defaultConstraints;
            _rigidbody.velocity = velocity;

            _trail.Play();
            // 投げた際の位置を保持する
            _startingPoint = transform.position;
            // 既に投げられたアイテムであるフラグを立てる
            IsThrowed = true;

            // 止まったらパーティクルも止まる
            OnStopAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_settings == null) return;

            // 床判定
            if (collision.gameObject.TryGetComponent(out FloorMarker _))
            {
                Crash();
            }

            // 既に投げられた状態
            if (IsThrowed)
            {
                // アイテムとぶつかった
                if (collision.gameObject.TryGetComponent(out ThrowedItem _))
                {
                    // 音鳴らす
                    Cri.PlaySE(_settings.HitSEName);
                }
                // キャラクターにぶつかった。子にコライダーがあり、親にスクリプトがある
                if (collision.transform.parent != null && 
                    collision.transform.parent.TryGetComponent(out Actor _))
                {
                    Crash();
                }
            }
        }

        /// <summary>
        /// 破裂させる
        /// </summary>
        void Crash()
        {
            // 音とパーティクルとデーカル
            Cri.PlaySE3D(transform.position, _settings.CrashSEName);
            Vector3 particlePosition = transform.position + _settings.CrashParticleOffset;
            ParticleMessageSender.SendMessage(_settings.CrashParticle, particlePosition);
            if (_decal != null) Instantiate(_decal, transform.position, _decal.transform.rotation);

            if (_decapu != null)
            {
                _decapu.SetActive(true);
                _decapu.transform.position = transform.position;
            }

            _pool.Return(this);
        }

        /// <summary>
        /// 注文としてキャッチされた際に呼ばれる
        /// </summary>
        public void Catch()
        {
            _pool.Return(this);
        }

        /// <summary>
        /// Rigidbodyを止める操作
        /// </summary>
        void Stop(bool isKinematic = false)
        {
            _rigidbody.isKinematic = isKinematic;
            if (!_rigidbody.isKinematic)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        /// <summary>
        /// 止まったらトレイルが止まる
        /// </summary>
        async UniTaskVoid OnStopAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => SqrSpeed < 1, cancellationToken: token);
            _trail.Stop();
        }
    }
}
