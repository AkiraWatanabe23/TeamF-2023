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

        protected override void OnFerverTimeEnter()
        {
            DanceIfStayStage();
        }

        protected override void Enter()
        {
            // ���Ƀt�B�[�o�[�Ȃ�A���̃X�e�[�g�Ńg���K�[�o���Ȃ��̂ł����Ń`�F�b�N����
            if (!DanceIfFerverTime()) Animator.Play("Walk");
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
        }
    }
}
