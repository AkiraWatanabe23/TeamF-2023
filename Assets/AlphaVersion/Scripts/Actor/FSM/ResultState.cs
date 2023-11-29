using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 注文の成功/失敗によって結果が確定した際に演出を行うステート
    /// </summary>
    public class ResultState : BaseState
    {
        [SerializeField] ActorSettingsSO _settings;
        [Header("アニメーションの再生時間")]
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

                return false; // 成功/失敗 以外の場合は適当に値を返す。
            }
        }

        public void Init(OrderResult result)
        {
            // 結果が 成功/失敗 以外の場合は例外を投げる
            if (!(result == OrderResult.Success || result == OrderResult.Failure))
            {
                throw new System.ArgumentException("注文結果が 成功/失敗 どちらでもない: " + result);
            }

            _result = result;
        }

        protected override void Enter()
        {
            PlayAnimation();
            PlayEffect();

            // スコアの送信
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
        /// 成功/失敗に対応したアニメーションの再生
        /// </summary>
        void PlayAnimation()
        {
            string name = string.Empty;
            if (_result == OrderResult.Success) name = "Success";
            if (_result == OrderResult.Failure) name = "Failure";

            Animator.Play(name);
        }

        /// <summary>
        /// 成功/失敗に対応した演出の再生
        /// </summary>
        void PlayEffect()
        {
            // パーティクル
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

            // 音
            if (_result == OrderResult.Success) Cri.PlaySE3D(transform.position, _settings.SuccessVoice);
            if (_result == OrderResult.Failure) Cri.PlaySE3D(transform.position, _settings.FailureVoice);
        }
    }
}
