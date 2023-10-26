using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げる方向と威力を視覚化する機能のクラス
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class ThrowPowerVisualizer : MonoBehaviour
    {
        [Header("矢印の長さの倍率")]
        [SerializeField] float _lengthPower = 1;

        LineRenderer _line;

        void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// 2点間の間にメッシュを描画する
        /// </summary>
        public void Draw(Vector3 startPoint, Vector3 goalPoint)
        {
            _line.enabled = true;

            _line.SetPosition(0, startPoint);
            _line.SetPosition(1, goalPoint);
        }

        /// <summary>
        /// 描画しているメッシュを削除する
        /// </summary>
        public void Delete()
        {
            _line.SetPosition(0, default);

            _line.enabled = false;
        }
    }
}
