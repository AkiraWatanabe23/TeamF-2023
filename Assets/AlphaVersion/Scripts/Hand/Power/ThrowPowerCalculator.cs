using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// カメラからテーブルへのレイキャストを用いて、飛ばす距離と方向を計算するクラス。
    /// 投げる方向は指定した位置からマウスカーソルへのベクトル、
    /// 投げる威力はマウスカーソルが移動した距離で決まる。
    /// </summary>
    public class ThrowPowerCalculator : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] TableRaycaster _tableRaycaster;
        [SerializeField] DistanceEvaluate _evaluate;

        Vector3 _startCursorViewPoint;
        Vector3 _endCursorViewPoint;
        bool _isHolding;

        public Vector3 StartingPoint { get; private set; }
        public Vector3 EndingPoint { get; private set; }

        /// <summary>
        /// 始点から終点の方向に威力の分だけ移動した地点
        /// </summary>
        public Vector3 PowerSizePoint
        {
            get
            {
                float sqr = (_startCursorViewPoint - _endCursorViewPoint).sqrMagnitude;
                Vector3 normalized = (EndingPoint - StartingPoint).normalized;
                return StartingPoint + normalized * sqr;
            }
        }

        void Update()
        {
            if (_isHolding) UpdateEndingPoint();
        }

        /// <summary>
        /// 飛ばす方向を計算するために始点をセットする
        /// 同時に、マウスカーソルの移動量で威力を計算するためにマウスカーソルの位置を保持する。
        /// </summary>
        public void SetStartingPoint(Vector3 startingPoint)
        {
            _startCursorViewPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            StartingPoint = startingPoint;

            _isHolding = true;
        }

        /// <summary>
        /// 始点から終点に向かう速度ベクトルを計算する。
        /// 速さはマウスカーソルの移動量で決定される。
        /// </summary>
        /// <returns>始点から終点が向き、マウスカーソルが移動した距離を速度にしたベクトル</returns>
        public Vector3 CalculateThrowVelocity()
        {
            if (_isHolding)
            {
                _isHolding = false;

                // ビューポイント座標系の2点を評価関数に通して01にリマップする
                float remap = _evaluate.Evaluate(_endCursorViewPoint, _startCursorViewPoint);
                return (EndingPoint - StartingPoint) * _settings.Power * remap;

            }
            else
            {
                Debug.LogWarning("StartingPointをセットしていない");
                return default;
            }
        }

        /// <summary>
        /// マウスカーソルの位置へレイキャストを行い、レイが当たった場所を基準とし、
        /// 終点のy座標を始点と同じ値にすることで、水平面上の点に補正する。
        /// 同時に、マウスカーソルの移動量で威力を計算するためにマウスカーソルの位置を更新する。
        /// </summary>
        void UpdateEndingPoint()
        {
            _tableRaycaster.CameraToMousePointRay(out Vector3 rayHitPoint);
            EndingPoint = new Vector3(rayHitPoint.x, StartingPoint.y, rayHitPoint.z);

            // NOTE:本来ならばマウスカーソルを離した際にのみ更新すればよいが、
            //      威力を視覚化するために同時に更新している。
            _endCursorViewPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        void OnDrawGizmos()
        {
            Draw2Points();
        }

        /// <summary>
        /// 始点と終点、2点を結ぶ線を描画する
        /// </summary>
        void Draw2Points()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(StartingPoint, 0.05f);
            Gizmos.DrawWireSphere(EndingPoint, 0.05f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(StartingPoint, EndingPoint);
        }
    }
}
