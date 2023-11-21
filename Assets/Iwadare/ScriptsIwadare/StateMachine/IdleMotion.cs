using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("ãxé~ÅIèÄîıäÆóπÅI", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.IdleAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);

    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);
    }
}
