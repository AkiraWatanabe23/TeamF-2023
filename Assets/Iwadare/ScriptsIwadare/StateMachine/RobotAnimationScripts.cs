using StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class RobotAnimationScripts : MonoBehaviour
{
    [Header("お客さんのアニメーター")]
    [SerializeField]
    private Animator _animator;


    [Header("AnimationStateMachine")]
    [SerializeField]
    private StateMachineController _stateMachine;
    [Header("デバック用(アタッチしなくても良い)")]
    [SerializeField]
    private Button _changeSitChairButton;

    [Header("SitScript全部を入れる。")]
    [SerializeField]
    private SitScripts[] _allSitScripts;

    [Header("デバック用(基本false)")]
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
        //デバック用
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
        //デバック用
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

    /// <summary>歩きアニメーション</summary>
    public void WalkAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetWalk);
    }

    /// <summary>座るアニメーション</summary>
    public void SitAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetSit);
    }

    /// <summary>成功アニメーション</summary>
    public void SuccessAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetSuccessMotion);
    }

    /// <summary>失敗アニメーション</summary>
    public void FailedAnimation()
    {
        _stateMachine.OnChangeState(_stateMachine.GetFailedMotion);
    }

    /// <summary>ダンスアニメーション</summary>
    public void DanceAnimation()
    {
        _stateMachine.FalseFeverTimeBool();
        _stateMachine.OnChangeState(_stateMachine.GetDance);
        _stateMachine.FeverTimeBool();
    }

    /// <summary>成功モーションと歩きモーションのAnimationEnd表示用</summary>
    public void WaitState()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSit)
        {
            _stateMachine.OnChangeState(_stateMachine.GetWaitState);
        }
    }

    /// <summary>Idleアニメーション(IdleのAnimationの中身はなし。)</summary>
    public void IdleState()
    {
        if(_stateMachine.CurrentState != _stateMachine.GetIdleState)
        {
            _stateMachine.OnChangeState(_stateMachine.GetIdleState);
        }
    }

    /// <summary>座る場所指定(引数SitScripts)</summary>
    /// <param name="sitScripts">SitScripts</param>
    public void SitReceipt(SitScripts sitScripts)
    {
        _stateMachine._sitScripts = sitScripts;
        _chairCount = (_chairCount + 1) % _allSitScripts.Length;
        Debug.Log(_chairCount);
    }

    /// <summary>座る場所指定(引数int)</summary>
    public void SitReceipt(int index)
    {
        _stateMachine._sitScripts = _allSitScripts[index];
        Debug.Log(index);
    }
}
