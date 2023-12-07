using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("çUåÇÅIèÄîıäÆóπÅI", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.AttackAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);

    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.AttackAniName + "End");
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);
    }
}
