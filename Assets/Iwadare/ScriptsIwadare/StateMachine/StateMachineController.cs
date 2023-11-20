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
        private SuccessMotionScript _successMotion = new();
        private FailedMotion _failedMotion = new();
        private DanceMotion _dance = new();
        private WaitState _waitState = new();
        public WalkMotion GetWalk => _walk;
        public SitMotion GetSit => _sit;
        public SuccessMotionScript GetSuccessMotion => _successMotion;
        public FailedMotion GetFailedMotion => _failedMotion;
        public DanceMotion GetDance => _dance;
        public WaitState GetWaitState => _waitState;
        private IState _currentState = null;
        public IState CurrentState => _currentState;

        [Header("アニメーションの名前")]
        [SerializeField] private string _walkAniName = "Walk";
        [SerializeField] private string _sitAniName = "Sitting";
        [SerializeField] private string _successAniName = "Surprized";
        [SerializeField] private string _failedAniName = "Failed";
        [SerializeField] private string _danceAniName = "Dance";
        public string WalkName => _walkAniName;
        public string SitName => _sitAniName;
        public string SuccessName => _successAniName;
        public string FailedName => _failedAniName;
        public string DanceName => _danceAniName;

        private float _time;
        /// <summary>外部参照</summary>
        private Animator _anim;
        public Animator Anim => _anim;

        public SitScripts _sitScripts;
        [Header("お客さん(自身)の位置")]
        public Transform _avatorTrams;

        [Header("遊び用(基本false)")]
        [SerializeField]
        private bool _ngWordbool;

        [Header("StateのDebugLogの表示非表示")]
        [SerializeField]
        private bool _displayLog = true;
        public bool DisplayLog => _displayLog;
        public bool NGWordbool => _ngWordbool;

        private bool _feverBool;

        // Start is called before the first frame update
        public void Init(ref Animator anim)
        {
            _anim = anim;
            IState[] state = new IState[6] { _walk, _sit, _successMotion, _failedMotion, _dance, _waitState };
            for (var i = 0; i < state.Length; i++)
            {
                _currentState = state[i];
                _currentState.InitialState(_displayLog);
            }
            DebugLogUtility.PrankLog("行くぞ！ステート戦隊！マシーンジャー！", _displayLog);
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
            if (!_feverBool)
            {
                _currentState.OnExitState(this);
                _currentState = state;
                _currentState.OnEnterState(this);
            }
        }

        public void FeverTimeBool()
        {
            _feverBool = true;
        }

        public void FalseFeverTimeBool()
        {
            _feverBool = false;
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
                    return _successMotion;
            }
            return null;
        }
    }
}
