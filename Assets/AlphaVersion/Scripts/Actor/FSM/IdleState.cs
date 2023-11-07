using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���̏�őҋ@����X�e�[�g
    /// </summary>
    public class IdleState : BaseState
    {
        public override StateType Type => StateType.Idle;

        protected override void Enter()
        {
            Animator.Play("Idle");
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
        }
    }
}
