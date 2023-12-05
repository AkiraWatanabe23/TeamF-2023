using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// インゲームの時間経過を表示するクラス
    /// 背景の大きさを0から1に変更することでゲージをマスクし、残り時間を表す
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] Transform _background;
        [SerializeField] Image _ferverGauge;

        void Awake()
        {
            //_background.localScale = Vector3.one;

            //// フィーバーゲージの長さ設定
            //float f = _settings.FerverTime / _settings.TimeLimit;
            //_ferverGauge.transform.localScale = new Vector3(f, 1, 1);
        }

        void Update()
        {
            // フィーバーのゲージを虹色にする
            //_ferverGauge.color = Color.HSVToRGB(Time.time % 1, 1, 1);
        }

        /// <summary>
        /// TransformのScaleを変更することで時間経過を表示する
        /// </summary>
        public void Draw(float max, float current)
        {
            //current = max - current;

            //Vector3 scale = transform.localScale;
            //scale.x = 1.0f - (current / max);

            //_background.localScale = scale;
        }
    }
}
