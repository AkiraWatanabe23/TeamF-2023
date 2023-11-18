using StateMachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitState : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("ë“ã@ÅIèÄîıäÆóπÅI",disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "":"",stateMachine.DisplayLog);
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
