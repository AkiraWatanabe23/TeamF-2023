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
        [SerializeField] BackDancer _dancer;
        [SerializeField] LightRotate _light;
        [SerializeField] GameObject _money;

        protected override void OnAwakeOverride()
        {
            // �Q�[���I�[�o�[���Ɏ~�߂Ĕ�\���ɂ���
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
            _dancer.Play();
            _light.Play();
            _money.SetActive(true);
        }

        protected override void OnFerverTimeExit()
        {
            _money.SetActive(false);
            _light.Stop();
        }
    }
}
