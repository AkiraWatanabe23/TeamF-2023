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
        Debug.Log("歩く！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("歩きはじめるぜ！");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("うおおおおおお！");
    }

    
    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("はあ...はあ...歩き終わったぜ...");
    }
}
