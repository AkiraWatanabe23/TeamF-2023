using System;
using UnityEngine;

namespace StateMachine
{
    [Serializable]
    public class StateMachineController
    {
        /// <summary>内部参照</summary>
        private WalkMotion _walk = new();
        private SitMotion _sit = new();
        private EmotionScript _emotion = new();
        private DanceMotion _dance = new();
        public WalkMotion GetWalk => _walk;
        public SitMotion GetSit => _sit;
        public EmotionScript GetEmotion => _emotion;
        public DanceMotion GetDance => _dance;
        private IState _currentState = null;
        public IState CurrentState => _currentState;

        [SerializeField] private string _walkAniName = "Walk";
        [SerializeField] private string _sitAniName = "Sitting";
        [SerializeField] private string _emotionAniName = "Surprized";
        [SerializeField] private string _danceAniName = "Dance";
        public string WalkName => _walkAniName;
        public string SitName => _sitAniName;
        public string EmotionName => _emotionAniName;
        public string DanceName => _danceAniName;

        private float _time;
        /// <summary>外部参照</summary>
        private Animator _anim;
        public Animator Anim => _anim;

        [SerializeField] public SitScripts _sitScripts;

        public Transform _avatorTrams;

        // Start is called before the first frame update
        public void Init(ref Animator anim)
        {
            _anim = anim;
            IState[] state = new IState[3] { _walk, _sit, _emotion };
            for (var i = 0; i < state.Length; i++)
            {
                _currentState = state[i];
                _currentState.InitialState();
            }
            Debug.Log("行くぞ！ステート戦隊！マシーンジャー！");
            _currentState = _walk;
            _currentState.OnEnterState(this);
        }

        // Update is called once per frame
        public void Update()
        {
            _time += Time.deltaTime;
            if (_time > 1f)
            {
                _currentState.OnUpdate(this);
                _time = 0f;
            }
        }
        public void OnChangeState(IState state)
        {
            _currentState.OnExitState(this);
            _currentState = state;
            _currentState.OnEnterState(this);
        }

        public IState GetState(MotionState state)
        {
            switch (state)
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
