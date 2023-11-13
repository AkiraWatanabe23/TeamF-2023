using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// タンブルウィード本体のクラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Tumbleweed : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [Header("床の高さ")]
        [SerializeField] float _floorHeight = 0.2f;
        [Header("床に落下後、消えるまでのディレイ")]
        [SerializeField] float _floorFalledDelay = 1.0f;

        CancellationTokenSource _cts = new();
        TumbleweedPool _pool;

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// 生成してプールに追加した際に1度だけプール側から呼び出されるメソッド
        /// </summary>
        public void OnCreate(TumbleweedPool pool)
        {
            _pool = pool;
        }

        /// <summary>
        /// 外部からプールから取り出した際に落下させるためのメソッド
        /// </summary>
        public void Fall()
        {
            _cts = new();
            PlayAsync(_cts.Token).Forget();
        }

        /// <summary>
        /// 床の高さに到達後、しばらくしたらプールに戻す
        /// </summary>
        async UniTaskVoid PlayAsync(CancellationToken token)
        {
            ValidPhisics();

            // 床に落ちるまで
            await UniTask.WaitUntil(() => transform.position.y <= _floorHeight, cancellationToken: token);
            // しばらくしたら消える
            await UniTask.Delay(System.TimeSpan.FromSeconds(_floorFalledDelay), cancellationToken: token);

            InvalidPhisics();
            _pool.Return(this);
        }

        /// <summary>
        /// 落下させるタイミングで物理挙動を有効化
        /// </summary>
        void ValidPhisics()
        {
            _rigidbody.isKinematic = false;
        }

        /// <summary>
        /// プールに戻すタイミングで物理挙動を無効化
        /// </summary>
        void InvalidPhisics()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
        }
    }
}
