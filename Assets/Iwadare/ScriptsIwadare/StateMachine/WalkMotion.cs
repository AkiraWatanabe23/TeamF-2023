using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class WalkMotion : IState
{
    MotionState _state = MotionState.Walk;
    public void InitialState(StateMachineController stateMachine)
    {

    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("�����͂��߂邺�I");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("���������������I");
    }

    
    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("�͂�...�͂�...�����I�������...");
    }
}
