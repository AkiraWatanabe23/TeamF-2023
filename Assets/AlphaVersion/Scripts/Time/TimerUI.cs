using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// インゲームの時間経過を表示するクラス
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] Transform _gauge;

        void Awake()
        {
            _gauge.localScale = Vector3.one;
        }

        /// <summary>
        /// TransformのScaleを変更することで時間経過を表示する
        /// </summary>
        public void Draw(float max, float current)
        {
            current = max - current;

            Vector3 scale = transform.localScale;
            scale.x = current / max;

            _gauge.localScale = scale;
        }
    }
}
