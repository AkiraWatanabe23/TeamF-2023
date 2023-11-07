using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    public enum Tension
    {
        Normal,
        Ferver,
    }

    /// <summary>
    /// フィーバータイムの開始/終了を検知する機能を提供するクラス
    /// このクラスを継承する場合は Awake は使用しないこと。
    /// </summary>
    public class FerverHandler : MonoBehaviour
    {
        Tension _tension = Tension.Normal; // ここ修正

        protected Tension Tension => _tension;

        void Awake()
        {
            // メッセージを受信したタイミングで開始/終了のコールバックをそれぞれ呼ぶ
            MessageBroker.Default.Receive<FerverTimeMessage>().Subscribe(msg =>
            {
                // 状態が2種類しかない前提の切り替え
                _tension = 1 - _tension;

                if (_tension == Tension.Ferver) OnFerverTimeEnter();
                else OnFerverTimeExit();
            }).AddTo(gameObject);

            OnAwakeOverride();
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnFerverTimeEnter() { }
        protected virtual void OnFerverTimeExit() { }
    }
}
