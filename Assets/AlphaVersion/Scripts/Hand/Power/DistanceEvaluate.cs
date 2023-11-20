using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Alpha
{
    /// <summary>
    /// 2点間のベクトルの距離を評価関数を用いて補正を行うクラス
    /// </summary>
    [System.Serializable]
    public class DistanceEvaluate
    {
        const float Max = 1.40f;
        const float Min = 0.01f;

        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// ViewPoint座標系(0,0 ~ 1,1)の2点の距離を評価関数に通し、補正された値を返す
        /// </summary>
        /// <returns>0~1の値</returns>
        public float Evaluate(Vector3 viewPointA, Vector3 viewPointB)
        {
            // 0~1 のxy平面上の座標なので距離は 0 から 約1.4 になる
            float time = (viewPointB - viewPointA).magnitude;
            time = math.remap(Min, Max, 0, 1, time);

            return time * _settings.Evaluate.Evaluate(time);
        }
    }
}