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
        DebugLogUtility.PrankLog("歩く！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName);
        DebugLogUtility.PrankLog("歩きはじめるぜ！");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("うおおおおおお！");
    }

    
    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("はあ...はあ...歩き終わったぜ...");
    }
}
