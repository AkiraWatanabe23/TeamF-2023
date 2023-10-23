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
        Debug.Log("ï‡Ç´ÇÕÇ∂ÇﬂÇÈÇ∫ÅI");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("Ç§Ç®Ç®Ç®Ç®Ç®Ç®ÅI");
    }

    
    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("ÇÕÇ†...ÇÕÇ†...ï‡Ç´èIÇÌÇ¡ÇΩÇ∫...");
    }
}
