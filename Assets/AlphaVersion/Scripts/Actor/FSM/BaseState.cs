using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum StateType
    {
        Base,
        Idle,
        Move,
        Order,
        Result,
        Fire,
        Animation,
    }

    /// <summary>
    /// 各ステートの基底クラス
    /// </summary>
    public abstract class BaseState : FerverHandler
    {
        protected enum Stage
        {
            Enter,
            Stay,
            Exit,
        }

        [SerializeField] AnimationAdapter _animator;
        [SerializeField] Transform _model;

        Stage _stage;
        BaseState _nextState;

        public abstract StateType Type { get; }
        protected Stage CurrentStage => _stage;
        protected AnimationAdapter Animator => _animator;
        protected Transform Model => _model;

        /// <summary>
        /// 1度の呼び出しでステートの段階に応じてEnter() Stay() Exit()のうちどれか1つが実行される
        /// 引数に違うステートが渡された場合は遷移する
        /// </summary>
        /// <returns>次の呼び出しで実行されるステート</returns>
        public BaseState Step(BaseState nextState)
        {
            // 違うステートが渡された場合は、遷移を試す
            if (nextState != this) TryChangeState(nextState);

            if (_stage == Stage.Enter)
            {
                Enter();
                _stage = Stage.Stay;
            }
            else if (_stage == Stage.Stay)
            {
                Stay();
            }
            else if (_stage == Stage.Exit)
            {
                Exit();
                _stage = Stage.Enter;

                return _nextState;
            }

            return this;
        }

        protected abstract void Enter();
        protected abstract void Stay();
        protected abstract void Exit();

        /// <summary>
        /// Enter()が呼ばれてかつ、ステートの遷移処理を呼んでいない場合のみ遷移可能
        /// </summary>
        /// <returns>ステートの遷移の可否</returns>
        bool TryChangeState(BaseState nextState)
        {
            if (_stage == Stage.Enter)
            {
                Debug.LogWarning($"Enter()が呼ばれる前にステートを遷移することは不可能: {Type} 遷移先: {nextState}");
                return false;
            }
            else if (_stage == Stage.Exit)
            {
                Debug.LogWarning($"既に別のステートに遷移する処理が呼ばれている: {Type} 遷移先: {nextState}");
                return false;
            }

            _stage = Stage.Exit;
            _nextState = nextState;

            return true;
        }

        protected void Log()
        {
            string s = _nextState != null ? _nextState.ToString() : string.Empty;
            Debug.Log($"状態:{Type} ステージ:{_stage} 次:{s}");
        }

        /// <summary>
        /// もしステートが進行中の場合はダンスを再生する
        /// </summary>
        protected bool DanceIfStayStage()
        {
            if (CurrentStage == Stage.Stay) Animator.Play("Dance");
            return CurrentStage == Stage.Stay;
        }

        /// <summary>
        /// もしフィーバータイム中の場合はダンスを再生する
        /// </summary>
        protected bool DanceIfFerverTime()
        {
            if (Tension == Tension.Ferver) Animator.Play("Dance");
            return Tension == Tension.Ferver;
        }
    }
}
