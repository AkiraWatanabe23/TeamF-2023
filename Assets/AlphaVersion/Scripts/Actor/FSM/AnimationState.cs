using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �A�j���[�V�������Đ�����X�e�[�g
    /// Enter�Ɠ����ɃA�j���[�V�������Đ����A�w�肵�����Ԍo�ߌ�ɑJ�ڂ���
    /// </summary>
    public class AnimationState : BaseState
    {
        [SerializeField] protected string _animationName;
        [SerializeField] protected float _playTime;

        float _elapsed;

        public override StateType Type => StateType.Animation;
        public bool IsRunning { get; private set; }

        public void Init()
        {
            _elapsed = 0;
            IsRunning = true;
        }

        protected override void Enter()
        {
            Animator.Play(_animationName);
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
            _elapsed += Time.deltaTime;
            if (IsRunning && _elapsed > _playTime)
            {
                IsRunning = false;
            }
        }
    }
}
