using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class WalkMotion : IState
{
    //MotionState _state = MotionState.Walk;
    public void InitialState()
    {
        DebugLogUtility.PrankLog("�����I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName);
        DebugLogUtility.PrankLog("�����͂��߂邺�I");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("���������������I");
    }

    
    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�͂�...�͂�...�����I�������...");
    }
}
