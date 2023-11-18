using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���̃p�[�e�B�N���𐧌䂷��N���X
    /// ���̃p�[�e�B�N���̓v�[�����O���ꂸ�A�V�[���J�n���ɐ����ς݂̂��̂��Đ�/��~���s��
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        [Header("�f�U�C�i�[�A�Z�b�g")]
        [SerializeField] ParticleSystem _fall;

        protected override void OnAwakeOverride()
        {
            // �Q�[���I�[�o�[���Ɏ~�߂Ĕ�\���ɂ���
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
