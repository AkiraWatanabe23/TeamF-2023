using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _pool = pool;
            _defaultConstraints = GetComponent<Rigidbody>().constraints;
        }

        /// <summary>
        /// 外部からプールから取り出した際に初期化する、Awakeの代用メソッド
        /// </summary>
        public void Init(ItemSettingsSO settings)
        {
            IsThrowed = false;
            
            _settings = settings;

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            FreezeXZ();
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

            // 投げた際の位置を保持する
            _startingPoint = transform.position;
            // 既に投げられたアイテムであるフラグを立てる
            IsThrowed = true;
        }

        void OnCollisionEnter(Collision collision)
        {
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
            // 音とパーティクル
            Cri.PlaySE(_settings.CrashSEName);
            Vector3 particlePosition = transform.position + _settings.CrashParticleOffset;
            ParticleMessageSender.SendMessage(_settings.CrashParticle, particlePosition);

            _pool.Return(this);
        }

        /// <summary>
        /// 注文としてキャッチされた際に呼ばれる
        /// </summary>
        public void Catch()
        {
            _pool.Return(this);
        }
    }
}
