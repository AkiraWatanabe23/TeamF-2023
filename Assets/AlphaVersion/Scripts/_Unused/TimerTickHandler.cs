using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// インゲームの時間経過をハンドルするクラス
    /// </summary>
    public class TimerTickHandler : MonoBehaviour, ITickReceive
    {
        [SerializeField] TimerUI _timerUI;

        /// <summary>
        /// 時間の更新
        /// </summary>
        public void Tick(float max, float current)
        {
            _timerUI.Draw(max, current);
        }
    }
}