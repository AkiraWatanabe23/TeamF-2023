using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// 指定した2点間を移動する機能のクラス
    /// 毎フレーム進行方向にレイキャストし、障害物があるかをチェックする機能を持つ
    /// </summary>
    public class MoveBetween : MonoBehaviour
    {
        [SerializeField] Collider _collider;
        [Header("進行方向へのレイキャストの設定")]
        [SerializeField] LayerMask _actorLayer;
        [SerializeField] float _rayDistance = 10.0f;
        [SerializeField] float _eyeHeight = 0.75f;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// 引数の速度で from から to へ移動する
        /// 進行方向に障害物がある場合は、その場で止めることが出来る。
        /// </summary>
        public async UniTask MoveAsync(float speed, Vector3 from, Vector3 to, CancellationToken token, bool useCheck = true)
        {
            float lerpProgress = 0;
            float sqrDistance = (to - from).sqrMagnitude;
            while (lerpProgress < 1)
            {
                _transform.position = Vector3.Lerp(from, to, lerpProgress);

                if (IsClearForward(to))
                {
                    lerpProgress += speed * Time.deltaTime / sqrDistance;
                }

                await UniTask.Yield(cancellationToken: token);
            }
        }

        /// <summary>
        /// レイキャストで進行方向にキャラクターが存在するかを判定する。
        /// </summary>
        bool IsClearForward(Vector3 to)
        {
            if (Physics.Raycast(RayToNextVertex(to), out RaycastHit hit, _actorLayer))
            {
                // キャラクターの基底クラスのコンポーネントを持っているで判定
                if (hit.collider.TryGetComponent(out Actor actor) &&
                    actor.GetInstanceID() != GetInstanceID())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 現在地から to に向けてのレイを返す。
        /// </summary>
        Ray RayToNextVertex(Vector3 to)
        {
            // キャラクターの眼の位置を高さの基準に、水平方向へのRay
            Vector3 origin = _transform.position + Vector3.up * _eyeHeight;
            to = new Vector3(to.x, origin.y, to.z);
            Vector3 direction = (to - origin).normalized;

            return new Ray(origin, direction * _rayDistance);
        }
    }
}