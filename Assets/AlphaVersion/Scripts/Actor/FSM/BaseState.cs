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
    /// �e�X�e�[�g�̊��N���X
    /// </summary>
    public abstract class BaseState : MonoBehaviour
    {
        protected enum Stage
        {
            Enter,
            Stay,
            Exit,
        }

        [SerializeField] Animator _animator;
        [SerializeField] Transform _model;

        Stage _stage;
        BaseState _nextState;

        public abstract StateType Type { get; }
        protected Stage CurrentStage => _stage;
        protected Animator Animator => _animator;
        protected Transform Model => _model;

        void Awake()
        {
            OnAwakeOverride();
        }

        /// <summary>
        /// 1�x�̌Ăяo���ŃX�e�[�g�̒i�K�ɉ�����Enter() Stay() Exit()�̂����ǂꂩ1�����s�����
        /// �����ɈႤ�X�e�[�g���n���ꂽ�ꍇ�͑J�ڂ���
        /// </summary>
        /// <returns>���̌Ăяo���Ŏ��s�����X�e�[�g</returns>
        public BaseState Step(BaseState nextState)
        {
            // �Ⴄ�X�e�[�g���n���ꂽ�ꍇ�́A�J�ڂ�����
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

        protected virtual void OnAwakeOverride() { }
        protected abstract void Enter();
        protected abstract void Stay();
        protected abstract void Exit();

        /// <summary>
        /// Enter()���Ă΂�Ă��A�X�e�[�g�̑J�ڏ������Ă�ł��Ȃ��ꍇ�̂ݑJ�ډ\
        /// </summary>
        /// <returns>�X�e�[�g�̑J�ڂ̉�</returns>
        bool TryChangeState(BaseState nextState)
        {
            if (_stage == Stage.Enter)
            {
                Debug.LogWarning($"Enter()���Ă΂��O�ɃX�e�[�g��J�ڂ��邱�Ƃ͕s�\: {Type} �J�ڐ�: {nextState}");
                return false;
            }
            else if (_stage == Stage.Exit)
            {
                Debug.LogWarning($"���ɕʂ̃X�e�[�g�ɑJ�ڂ��鏈�����Ă΂�Ă���: {Type} �J�ڐ�: {nextState}");
                return false;
            }

            _stage = Stage.Exit;
            _nextState = nextState;

            return true;
        }

        protected void Log()
        {
            string s = _nextState != null ? _nextState.ToString() : string.Empty;
            Debug.Log($"���:{Type} �X�e�[�W:{_stage} ��:{s}");
        }
    }
}
