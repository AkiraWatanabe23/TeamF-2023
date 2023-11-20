using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���p�̉��o���s���N���X
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        [Header("�t�B�[�o�[���ɖ����������")]
        [SerializeField] GameObject _normalReflectionProbe;
        [SerializeField] GameObject _normalDirectionalLight;
        [Header("�t�B�[�o�[���ɗL���ɂȂ�")]
        [SerializeField] GameObject _ferverDirectionalLight;
        [SerializeField] GameObject _ferverSpotLight;
        [SerializeField] GameObject _ferverReflectionProbe;
        [SerializeField] GameObject _mirrorBall;
        [SerializeField] ParticleSystem _fallParticle;

        protected override void OnAwakeOverride()
        {
            // �Q�[���I�[�o�[���Ɏ~�߂Ĕ�\���ɂ���
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
