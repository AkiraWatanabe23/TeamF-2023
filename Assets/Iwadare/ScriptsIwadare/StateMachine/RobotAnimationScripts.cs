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
    private Transform _custmerTrans;

    [SerializeField]
    private StateMachineController _stateMachine;

    [SerializeField]
    Button _sitButton;

    [SerializeField]
    Button _surprisedButton;

    [SerializeField]
    SitScripts _sitScripts;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sitButton.onClick.AddListener(() => SitAnimation());
        _surprisedButton.onClick.AddListener(() => SuprizedAnimation());
        _stateMachine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
    }

    public void SitAnimation()
    {
        _sitBool = !_sitBool;
        if (_sitBool)
        {
            _animator.Play("Sitting");
            _custmerTrans.position = _sitScripts.SitDownPosition();
            _custmerTrans.rotation = Quaternion.EulerAngles(_sitScripts.SitDownRotation());
            _stateMachine.OnChangeState(_stateMachine.GetSit);
        }
        else
        {
            _custmerTrans.position = _sitScripts.StandUp();
            _animator.Play("Walk");
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }

    public void SuprizedAnimation()
    {
        _surprisedBool = !_surprisedBool;
        if (_surprisedBool)
        {
            _animator.Play("Surprized");
            _stateMachine.OnChangeState(_stateMachine.GetEmotion);
        }
        else
        {
            _animator.Play("Walk");
            _stateMachine.OnChangeState(_stateMachine.GetWalk);
        }
    }


}
