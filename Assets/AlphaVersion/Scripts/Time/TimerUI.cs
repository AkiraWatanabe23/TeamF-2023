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
        [SerializeField] Image _feverGauge;
        [SerializeField] Transform _under;
        [SerializeField] Transform _ferverUnder;
        [SerializeField] float _mag = 1.0f;
        [SerializeField] float _feverMag = 1.0f;

        void Update()
        {
            if (_ferverUnder.localScale.x > 0)
            {
                // フィーバーのゲージを虹色にする
                _feverGauge.color = Color.HSVToRGB(Time.time % 1, 1, 1);
            }
        }

        /// <summary>
        /// TransformのScaleを変更することで時間経過を表示する
        /// </summary>
        public void Draw(float max, float current)
        {
            //Debug.Log(current + " " + max);

            if (_under.localScale.x <= 1)
            {
                Vector3 scale = _under.localScale;
                scale.x += Time.deltaTime * _mag;
                _under.localScale = scale;
            }
            else
            {
                Vector3 scale = _ferverUnder.localScale;
                scale.x += Time.deltaTime * _feverMag;
                _ferverUnder.localScale = scale;
            }
        }
    }
}
