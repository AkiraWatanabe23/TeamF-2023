using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// マウスが移動したかどうかを判定するクラス
    /// 2点間の距離が閾値以下かどうかで判定を行う
    /// </summary>
    public class MouseMovementChecker : MonoBehaviour
    {
        [Header("移動していないと判定される距離")]
        [SerializeField] float Threshold = 0.001f;

        Vector3 _startingPoint;
        bool _isHolding;

        /// <summary>
        /// 現在のマウスカーソルの位置を始点とする
        /// </summary>
        public void SetStartingPoint()
        {
            _startingPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            _isHolding = true;
        }

        /// <summary>
        /// 始点から現在のマウスカーソルの座標までの距離が閾値以下なら動いていないと判定
        /// StartingPoint が設定されていない場合は true を返す
        /// </summary>
        public bool IsMoved()
        {
            if (_isHolding)
            {
                Vector3 endingPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                return (endingPoint - _startingPoint).sqrMagnitude > Threshold;
            }
            else
            {
                Debug.LogWarning("StartingPointをセットしていない");
                return true;
            }
        }
    }
}
