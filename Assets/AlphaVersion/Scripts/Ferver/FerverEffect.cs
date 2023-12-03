using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// フィーバータイム用の演出を行うクラス
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        protected override void OnAwakeOverride()
        {
            // ゲームオーバー時に止めて非表示にする
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => 
            {
                OnFerverTimeExit();
            }).AddTo(gameObject);
        }

        void Start()
        {
            OnFerverTimeExit();
        }

        protected override void OnFerverTimeEnter()
        {

        }

        protected override void OnFerverTimeExit()
        {

        }
    }
}
