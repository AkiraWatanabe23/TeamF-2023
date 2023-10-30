using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// メインカメラからテーブルへ向けたレイキャストを行うクラス。
    /// </summary>
    public class TableRaycaster : MonoBehaviour
    {
        [Header("テーブルへ飛ばすレイの設定")]
        [SerializeField] LayerMask _tableLayer;
        [SerializeField] float _distance = 100;

        /// <summary>
        /// メインカメラからマウスカーソルの位置へレイキャストして判定を行う。
        /// rayHitPoint にはテーブル以外にヒットした場合でもその座標が代入される。
        /// しなかった場合はdefaultが代入される。
        /// </summary>
        /// <returns>レイがテーブルにヒットした:true しなかった:false</returns>
        public bool CameraToMousePointRay(out Vector3 rayHitPoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ギズモへ描画
            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.blue);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _tableLayer))
            {
                rayHitPoint = hitInfo.point;
                // テーブルのコンポーネントを持っているかで判定
                return hitInfo.collider.TryGetComponent(out TableMarker _);
            }
            else
            {
                rayHitPoint = default;
                return false;
            }
        }
    }
}