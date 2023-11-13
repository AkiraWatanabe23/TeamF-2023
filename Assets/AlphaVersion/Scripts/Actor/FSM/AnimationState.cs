using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// アニメーションを再生するステート
    /// </summary>
    public class AnimationState : BaseState
    {
        [SerializeField] string _animationName;
        [SerializeField] float _playTime;

        float _elapsed;

        public override StateType Type => StateType.Animation;
        public bool IsRunning { get; private set; }

        protected override void Enter()
        {
            Animator.Play(_animationName);
            _elapsed = 0;
            IsRunning = true;
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _playTime)
            {
                IsRunning = false;
            }
        }
    }
}
