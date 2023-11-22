using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// その場で待機するステート
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
            // 既にフィーバーなら、このステートでトリガー出来ないのでここでチェックする
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
