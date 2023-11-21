using StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class RobotAnimationScripts : MonoBehaviour
{
    [Header("���q����̃A�j���[�^�[")]
    [SerializeField]
    private Animator _animator;


    [Header("AnimationStateMachine")]
    [SerializeField]
    private StateMachineController _stateMachine;
    [Header("�f�o�b�N�p(�A�^�b�`���Ȃ��Ă��ǂ�)")]
    [SerializeField]
    private Button _changeSitChairButton;

    [Header("SitScript�S��������B")]
    [SerializeField]
    private SitScripts[] _allSitScripts;

    [Header("�f�o�b�N�p(��{false)")]
    [SerializeField]
    bool _animationCallBackTestBool;

    private SitRequest _sitRequest;

    private int _chairCount = 0;

    void Start()
    {
        _sitRequest = FindObjectOfType<SitRequest>();
        _allSitScripts = _sitRequest.SitScriptsRequest();
        if (_changeSitChairButton != null) { _changeSitChairButton.onClick.AddListener(() => SitReceipt(_allSitScripts[(_chairCount + 1) % _allSitScripts.Length])); }
        _stateMachine.Init(ref _animator);
    }

    private void OnEnable()
    {
        //�f�o�b�N�p
        if (_animationCallBackTestBool)
        {
            AnimationCallBackTest.OnAnimationWalk += WalkAnimation;
            AnimationCallBackTest.OnAnimationSit += SitAnimation;
            AnimationCallBackTest.OnAnimationSuccess += SuccessAnimation;
            AnimationCallBackTest.OnAnimationFailed += FailedAnimation;
            AnimationCallBackTest.OnAnimationStay += WaitState;
            AnimationCallBackTest.OnAnimationIdle += IdleState;
        }
    }

    private void OnDisable()
    {
        //�f�o�b�N�p
        if (_animationCallBackTestBool)
        {
            AnimationCallBackTest.OnAnimationWalk -= WalkAnimation;
            AnimationCallBackTest.OnAnimationSit -= SitAnimation;
            AnimationCallBackTest.OnAnimationSuccess -= SuccessAnimation;
            AnimationCallBackTest.OnAnimationFailed -= FailedAnimation;
            AnimationCallBackTest.OnAnimationStay -= WaitState;
            AnimationCallBackTest.OnAnimationIdle -= IdleState;
        }
    }

    void Update()
    {
        _stateMachine.Update();
    }

    /// <summary>�����A�j���[�V����</summary>
    public void WalkAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetWalk);
    }

    /// <summary>����A�j���[�V����</summary>
    public void SitAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetSit);
    }

    /// <summary>�����A�j���[�V����</summary>
    public void SuccessAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetSuccessMotion);
    }

    /// <summary>���s�A�j���[�V����</summary>
    public void FailedAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetFailedMotion);
    }

    /// <summary>�_���X�A�j���[�V����</summary>
    public void DanceAnimation()
    {
        _stateMachine.FalseFeverTimeBool();
        _stateMachine.OnChangeState(_stateMachine.GetDance);
        _stateMachine.FeverTimeBool();
    }

    /// <summary>�������[�V�����ƕ������[�V������AnimationEnd�\���p</summary>
    public void WaitState()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSit)
        {
            _stateMachine.OnChangeState(_stateMachine.GetWaitState);
        }
    }

    /// <summary>Idle�A�j���[�V����(Idle��Animation�̒��g�͂Ȃ��B)</summary>
    public void IdleState()
    {
        if(_stateMachine.CurrentState != _stateMachine.GetIdleState)
        {
            _stateMachine.OnChangeState(_stateMachine.GetIdleState);
        }
    }

    /// <summary>����ꏊ�w��(����SitScripts)</summary>
    /// <param name="sitScripts">SitScripts</param>
    public void SitReceipt(SitScripts sitScripts)
    {
        _stateMachine._sitScripts = sitScripts;
        _chairCount = (_chairCount + 1) % _allSitScripts.Length;
        Debug.Log(_chairCount);
    }

    /// <summary>����ꏊ�w��(����int)</summary>
    public void SitReceipt(int index)
    {
        _stateMachine._sitScripts = _allSitScripts[index];
        Debug.Log(index);
    }
}
