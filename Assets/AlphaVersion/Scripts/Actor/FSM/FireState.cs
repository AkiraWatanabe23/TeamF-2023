using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// ��莞�Ԍo�ߌ�Ɏˌ����s���X�e�[�g
    /// </summary>
    public class FireState : BaseState
    {
        public override StateType Type => StateType.Fire;
        public bool IsRunning { get; private set; }

        // TODO:���Ń^�C�}�[���ɂ��Ă����A���Ԑ؂�Ŋ���
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
            // ���Ԍo�߂Ŋ����t���O������
            _elapsed += Time.deltaTime;
            if (_elapsed >= _timeLimit)
            {
                _elapsed = 0;
                IsRunning = false;
            }
        }
    }
}