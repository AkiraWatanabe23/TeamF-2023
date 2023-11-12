using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotAnimationScripts : MonoBehaviour
{
    bool _sitBool;
    bool _surprisedBool;
    Animator _animator;

    [SerializeField]
    private StateMachineController _stateMachine;

    [SerializeField]
    Button _sitButton;

    [SerializeField]
    Button _successButton;
    
    [SerializeField] 
    private Button _failedButton;

    [SerializeField]
    Button _danceButton;

    [SerializeField]
    Button _changeSitChairButton;

    [SerializeField]
    SitScripts[] _allSitScripts;

    int _chairCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_sitButton != null) { _sitButton.onClick.AddListener(() => SitAnimation()); }
        if (_successButton != null) { _successButton.onClick.AddListener(() => SuccessAnimation()); }
        if (_danceButton != null) { _danceButton.onClick.AddListener(() => DanceAnimation()); }
        if(_changeSitChairButton != null) { _changeSitChairButton.onClick.AddListener(() => SitReceipt(_allSitScripts[(_chairCount + 1) % _allSitScripts.Length])); }
        if(_failedButton != null) {_failedButton.onClick.AddListener(() => FailedAnimation()); }
        _stateMachine.Init(ref _animator);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
    }

    public void SitAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSit)
        {
            _stateMachine.OnChangeState(_stateMachine.GetSit);
        }
        else
        {
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    public void SuccessAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetSuccessMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetSuccessMotion);
        }
        else
        {
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    public void FailedAnimation()
    {
        if(_stateMachine.CurrentState != _stateMachine.GetFailedMotion)
        {
            _stateMachine.OnChangeState(_stateMachine.GetFailedMotion);
        }
        else
        {
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    public void DanceAnimation()
    {
        if (_stateMachine.CurrentState != _stateMachine.GetDance)
        {
            _stateMachine.OnChangeState(_stateMachine.GetDance);
        }
        else
        {
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    public void SitReceipt(SitScripts sitScripts)
    {
        _stateMachine._sitScripts = sitScripts;
        _chairCount = (_chairCount + 1) % _allSitScripts.Length;
        Debug.Log(_chairCount);
    }

    public void SitReceipt(int index)
    {
        _stateMachine._sitScripts = _allSitScripts[index];
        Debug.Log(index);
    }
}
