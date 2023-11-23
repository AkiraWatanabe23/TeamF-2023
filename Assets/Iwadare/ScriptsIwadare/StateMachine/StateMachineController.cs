using System;
using UnityEngine;

namespace StateMachine
{
    [Serializable]
    public class StateMachineController
    {
        /// <summary>�����Q��</summary>
        private WalkMotion _walk = new();
        private SitMotion _sit = new();
        private SuccessMotionScript _successMotion = new();
        private FailedMotion _failedMotion = new();
        private DanceMotion _dance = new();
        private WaitState _waitState = new();
        private IdleMotion _idleState = new();
        private AttackMotion _attackMotion = new();
        public WalkMotion GetWalk => _walk;
        public SitMotion GetSit => _sit;
        public SuccessMotionScript GetSuccessMotion => _successMotion;
        public FailedMotion GetFailedMotion => _failedMotion;
        public DanceMotion GetDance => _dance;
        public WaitState GetWaitState => _waitState;
        public IdleMotion GetIdleState => _idleState;
        public AttackMotion GetAttackMotion => _attackMotion;

        private IState _currentState = null;
        public IState CurrentState => _currentState;

        [Header("�A�j���[�V�����̖��O")]
        [SerializeField] private string _walkAniName = "Walk";
        [SerializeField] private string _sitAniName = "Order";
        [SerializeField] private string _successAniName = "Success";
        [SerializeField] private string _failedAniName = "Failure";
        [SerializeField] private string _danceAniName = "Dance";
        [SerializeField] private string _idleAniName = "Idle";
        [SerializeField] private string _attackAniName = "Attack";
        public string WalkName => _walkAniName;
        public string SitName => _sitAniName;
        public string SuccessName => _successAniName;
        public string FailedName => _failedAniName;
        public string DanceName => _danceAniName;
        public string IdleAniName => _idleAniName;

        public string AttackAniName => _attackAniName;

        private float _time;
        /// <summary>�O���Q��</summary>
        private Animator _anim;
        public Animator Anim => _anim;

        public SitScripts _sitScripts;
        [Header("���q����(���g)�̈ʒu")]
        public Transform _avatorTrams;

        [Header("�V�їp(��{false)")]
        [SerializeField]
        private bool _ngWordbool;

        [Header("State��DebugLog�̕\����\��")]
        [SerializeField]
        private bool _displayLog = true;
        public bool DisplayLog => _displayLog;
        public bool NGWordbool => _ngWordbool;

        private bool _feverBool;

        public void Init(ref Animator anim)
        {
            _anim = anim;
            IState[] state = new IState[8] { _walk, _sit, _successMotion, _failedMotion, _dance, _waitState, _idleState ,_attackMotion};
            for (var i = 0; i < state.Length; i++)
            {
                _currentState = state[i];
                _currentState.InitialState(_displayLog);
            }
            DebugLogUtility.PrankLog("�s�����I�X�e�[�g����I�}�V�[���W���[�I", _displayLog);
            _currentState = _waitState;
            _currentState.OnEnterState(this);
        }

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

        //public void FeverTimeBool()
        //{
        //    _feverBool = true;
        //}

        //public void FalseFeverTimeBool()
        //{
        //    _feverBool = false;
        //}


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
