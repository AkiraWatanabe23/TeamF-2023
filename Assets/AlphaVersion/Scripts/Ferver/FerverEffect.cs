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
        [Header("フィーバー時に無効化される")]
        [SerializeField] GameObject _normalReflectionProbe;
        [SerializeField] GameObject _normalDirectionalLight;
        [Header("フィーバー時に有効になる")]
        [SerializeField] GameObject _ferverDirectionalLight;
        [SerializeField] GameObject _ferverSpotLight;
        [SerializeField] GameObject _ferverReflectionProbe;
        [SerializeField] GameObject _mirrorBall;
        [SerializeField] ParticleSystem _fallParticle;

        protected override void OnAwakeOverride()
        {
            // ゲームオーバー時に止めて非表示にする
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => 
            {
                OnFerverTimeExit();
                _fallParticle.gameObject.SetActive(false);
            }).AddTo(gameObject);
        }

        void Start()
        {
            OnFerverTimeExit();
        }

        protected override void OnFerverTimeEnter()
        {
            _normalReflectionProbe.SetActive(false);
            _normalDirectionalLight.SetActive(false);
            _ferverDirectionalLight.SetActive(true);
            _ferverSpotLight.SetActive(true);
            _ferverReflectionProbe.SetActive(true);
            _mirrorBall.SetActive(true);
            _fallParticle.Play();
        }

        protected override void OnFerverTimeExit()
        {
            _normalReflectionProbe.SetActive(true);
            _normalDirectionalLight.SetActive(true);
            _ferverDirectionalLight.SetActive(false);
            _ferverSpotLight.SetActive(false);
            _ferverReflectionProbe.SetActive(false);
            _mirrorBall.SetActive(false);
            _fallParticle.Stop();
        }
    }
}
