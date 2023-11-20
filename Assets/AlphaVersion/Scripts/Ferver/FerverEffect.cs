using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムのパーティクルを制御するクラス
    /// このパーティクルはプーリングされず、シーン開始時に生成済みのものを再生/停止を行う
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        [Header("デザイナーアセット")]
        [SerializeField] ParticleSystem _fall;

        protected override void OnAwakeOverride()
        {
            // ゲームオーバー時に止めて非表示にする
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => 
            {
                _fall.Stop();
                _fall.gameObject.SetActive(false);
            }).AddTo(gameObject);
        }

        protected override void OnFerverTimeEnter()
        {
            _fall.Play();
        }

        protected override void OnFerverTimeExit()
        {
            _fall.Stop();
        }
    }
}
