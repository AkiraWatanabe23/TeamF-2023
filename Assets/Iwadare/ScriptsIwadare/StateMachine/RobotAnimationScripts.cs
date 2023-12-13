using DG.Tweening;
using StateMachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RobotAnimationScripts : MonoBehaviour
{
    [Header("���q����̃A�j���[�^�[")]
    [SerializeField]
    private Animator _animator;
    [SerializeField] float _attackTime = 3f;
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

    Sequence _attackSeq;

    public UnityAction<float> OnWaitAction;

    void Awake()
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
            AnimationCallBackTest.OnAnimationAttack += AttackMotion;
            AnimationCallBackTest.OnAnimationHits += HitsMotion;
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
            AnimationCallBackTest.OnAnimationAttack -= AttackMotion;
            AnimationCallBackTest.OnAnimationHits -= HitsMotion;
        }
    }

    void Update()
    {
        _stateMachine.Update();
    }

    /// <summary>�����A�j���[�V����</summary>
    public void WalkAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetWalk)
        {
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    /// <summary>����A�j���[�V����</summary>
    public void SitAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSit)
        {
            _stateMachine.OnChangeState(_stateMachine.GetSit);
        }
    }

    /// <summary>�����A�j���[�V����</summary>
    public void SuccessAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSuccessMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetSuccessMotion);
        }
    }

    /// <summary>���s�A�j���[�V����</summary>
    public void FailedAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetFailedMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetFailedMotion);
        }
    }

    /// <summary>�_���X�A�j���[�V����</summary>
    public void DanceAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetDance)
        {
            _stateMachine.OnChangeState(_stateMachine.GetDance);
        }
    }

    /// <summary>�������[�V�����ƕ������[�V������AnimationEnd�\���p</summary>
    public void WaitState()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSit &&
            _stateMachine.CurrentState != _stateMachine.GetWaitState)
        {
            _stateMachine.OnChangeState(_stateMachine.GetWaitState);
        }
    }

    /// <summary>Idle�A�j���[�V����</summary>
    public void IdleState()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetIdleState)
        {
            _stateMachine.OnChangeState(_stateMachine.GetIdleState);
        }
    }

    /// <summary>Attack�A�j���[�V����</summary>
    public void AttackMotion()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetAttackMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetAttackMotion);
            EndAction(_attackTime);
        }
    }

    public void EndAction(float time)
    {
        _attackSeq = DOTween.Sequence();
        _attackSeq.AppendInterval(_attackTime)
            .AppendCallback(() => { EndAttackState(); })
            .OnUpdate(() =>
            {
                if (_stateMachine.CurrentState != _stateMachine.GetAttackMotion)
                {
                    this.DOKill();
                }
            });
        _attackSeq.Play().SetLink(gameObject).SetId(this);
    }

    public void EndAttackState()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetAttackMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetWaitState);
        }
    }

    /// <summary>Hits���[�V����</summary>
    public void HitsMotion()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetHitsMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetHitsMotion);
        }
    }

    /// <summary>����ꏊ�w��(����SitScripts)</summary>
    /// <param name="sitScripts">SitScripts</param>
    public void SitReceipt(SitScripts sitScripts)
    {
        _stateMachine._sitScripts = sitScripts;
        _chairCount = (_chairCount + 1) % _allSitScripts.Length;
    }

    /// <summary>����ꏊ�w��(����int)</summary>
    public void SitReceipt(int index)
    {
        _stateMachine._sitScripts = _allSitScripts[index];
    }
}
