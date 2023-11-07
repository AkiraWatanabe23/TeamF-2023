using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 移動方向に障害物があるかをレイキャストを用いて判定する機能のクラス
    /// </summary>
    public class MoveRaycaster : MonoBehaviour
    {
        [Header("前方を調べるレイの設定")]
        [SerializeField] float _rayDistance = 1.0f;
        [SerializeField] float _eyeHeight = 0.75f;
        [SerializeField] LayerMask _actorLayer;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// レイキャストで進行方向にキャラクターが存在するかを判定する。
        /// </summary>
        public bool IsClearForward(Vector3 to)
        {
            if (Physics.Raycast(GetRay(to), out RaycastHit hit, _rayDistance, _actorLayer))
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
        Ray GetRay(Vector3 to)
        {
            // キャラクターの眼の位置を高さの基準に、水平方向へのRay
            Vector3 origin = _transform.position + Vector3.up * _eyeHeight;
            to = new Vector3(to.x, origin.y, to.z);
            Vector3 direction = (to - origin).normalized;

            // ギズモに描画
            Debug.DrawRay(origin, direction * _rayDistance, Color.red);

            return new Ray(origin, direction);
        }
    }
}
