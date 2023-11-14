using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���̃p�[�e�B�N���𐧌䂷��N���X
    /// ���̃p�[�e�B�N���̓v�[�����O���ꂸ�A�V�[���J�n���ɐ����ς݂̂��̂��Đ�/��~���s��
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        [Header("���삵������")]
        [SerializeField] ParticleSystem _left;
        [SerializeField] ParticleSystem _right;
        [Header("�f�U�C�i�[�A�Z�b�g")]
        [SerializeField] ParticleSystem _fall;

        protected override void OnAwakeOverride()
        {
        }

        protected override void OnFerverTimeEnter()
        {
            //_left.Play();
            //_right.Play();
            _fall.Play();
        }

        protected override void OnFerverTimeExit()
        {
            //_left.Stop();
            //_right.Stop();
            _fall.Play();
        }
    }
}
