using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    [Serializable]
    public class StateMachineController
    {
        WalkMotion _walk = new();
        SitMotion _sit = new();
        EmotionScript _emotion = new();
        public WalkMotion GetWalk => _walk;
        public SitMotion GetSit => _sit;
        public EmotionScript GetEmotion => _emotion;
        private IState _currentState = null;

        // Start is called before the first frame update
        public void Init()
        {
            IState[] state = new IState[3] {_walk,_sit,_emotion};
            for (var i = 0; i < state.Length; i++)
            {
                _currentState = state[i];
                _currentState.InitialState(this);
            }
            Debug.Log("行くぞ！ステート戦隊！マシーンジャー！");
            _currentState = _walk;
            _currentState.OnEnterState(this);
        }

        // Update is called once per frame
        public void Update()
        {
            _currentState.OnUpdate(this);
        }
        public void OnChangeState(IState state)
        {
            _currentState.OnExitState(this);
            _currentState = state;
            _currentState.OnEnterState(this);
        }

        public IState GetState(MotionState state)
        {
            switch(state)
            {
                case MotionState.Walk:
                    return _walk;
                case MotionState.Sit:
                    return _sit;
                case MotionState.Emotion:
                    return _emotion;
            }
            return null;
        }
    }
}
