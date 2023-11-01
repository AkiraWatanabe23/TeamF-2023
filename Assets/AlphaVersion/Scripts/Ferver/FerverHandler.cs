using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムの開始/終了を検知する機能を提供するクラス
    /// このクラスを継承する場合は Awake は使用しないこと。
    /// </summary>
    public class FerverHandler : MonoBehaviour
    {
        bool _isFerver = false; // ここ修正

        protected bool IsFerver => _isFerver;

        void Awake()
        {
            // メッセージを受信したタイミングで開始/終了のコールバックをそれぞれ呼ぶ
            MessageBroker.Default.Receive<FerverTimeMessage>().Subscribe(msg =>
            {
                _isFerver = !_isFerver;

                if (_isFerver) OnFerverTimeEnter();
                else OnFerverTimeExit();
            }).AddTo(gameObject);

            OnAwakeOverride();
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnFerverTimeEnter() { }
        protected virtual void OnFerverTimeExit() { }
    }
}
