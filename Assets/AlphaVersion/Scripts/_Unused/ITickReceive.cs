using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// インゲームのタイマーが進行する度に処理を行う機能のインターフェース
    /// </summary>
    public interface ITickReceive
    {
        public void Tick(float max, float current);
    }
}
