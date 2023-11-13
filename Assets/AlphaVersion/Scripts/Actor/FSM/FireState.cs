using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// 一定時間経過後に射撃を行うステート
    /// </summary>
    public class FireState : BaseState
    {
        public override StateType Type => StateType.Fire;
        public bool IsRunning { get; private set; }

        // TODO:仮でタイマー性にしておく、時間切れで完了
        float _elapsed;
        float _timeLimit = 2.0f;

        public void Init()
        {
            IsRunning = true;
            _elapsed = 0;
        }

        protected override void Enter()
        {
        }

        protected override void Exit()
        {
        }

        protected override void Stay()
        {
            // 時間経過で完了フラグが立つ
            _elapsed += Time.deltaTime;
            if (_elapsed >= _timeLimit)
            {
                _elapsed = 0;
                IsRunning = false;
            }
        }
    }
}