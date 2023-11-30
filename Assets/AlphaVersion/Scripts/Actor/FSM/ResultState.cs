using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �����̐���/���s�ɂ���Č��ʂ��m�肵���ۂɉ��o���s���X�e�[�g
    /// </summary>
    public class ResultState : BaseState
    {
        [SerializeField] ActorSettingsSO _settings;
        [Header("�A�j���[�V�����̍Đ�����")]
        [SerializeField] float _successPlayTime = 1.0f;
        [SerializeField] float _failurePlayTime = 1.0f;

        OrderResult _result;
        float _elapsed;

        public override StateType Type => StateType.Result;

        public bool IsRunning
        {
            get
            {
                if (_result == OrderResult.Success) return _elapsed <= _successPlayTime;
                if (_result == OrderResult.Failure) return _elapsed <= _failurePlayTime;

                return false; // ����/���s �ȊO�̏ꍇ�͓K���ɒl��Ԃ��B
            }
        }

        public void Init(OrderResult result)
        {
            // ���ʂ� ����/���s �ȊO�̏ꍇ�͗�O�𓊂���
            if (!(result == OrderResult.Success || result == OrderResult.Failure))
            {
                throw new System.ArgumentException("�������ʂ� ����/���s �ǂ���ł��Ȃ�: " + result);
            }

            _result = result;
        }

        protected override void Enter()
        {
            PlayAnimation();
            PlayEffect();

            // �X�R�A�̑��M
            OrderScoreSender.SendScore(_result, _settings.ActorType, Tension);
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
            _elapsed += Time.deltaTime;
        }

        /// <summary>
        /// ����/���s�ɑΉ������A�j���[�V�����̍Đ�
        /// </summary>
        void PlayAnimation()
        {
            string name = string.Empty;
            if (_result == OrderResult.Success) name = "Success";
            if (_result == OrderResult.Failure) name = "Failure";

            Animator.Play(name);
        }

        /// <summary>
        /// ����/���s�ɑΉ��������o�̍Đ�
        /// </summary>
        void PlayEffect()
        {
            // �p�[�e�B�N��
            ParticleType particle = default;
            Vector3 offset = default;
            if (_result == OrderResult.Success)
            {
                particle = _settings.SuccessParticle;
                offset = _settings.SuccessParticleOffset;
            }
            if (_result == OrderResult.Failure)
            {
                particle = _settings.FailureParticle;
                offset = _settings.FailureParticleOffset;
            }
            Vector3 position = transform.position + offset;
            ParticleMessageSender.SendMessage(particle, position, transform);

            // ��
            if (_result == OrderResult.Success) Cri.PlaySE3D(transform.position, _settings.SuccessVoice);
            if (_result == OrderResult.Failure) Cri.PlaySE3D(transform.position, _settings.FailureVoice);
        }
    }
}
