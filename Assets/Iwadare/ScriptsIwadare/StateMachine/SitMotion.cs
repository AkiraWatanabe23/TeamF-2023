using StateMachine;
using System;
using UnityEngine;

[Serializable]
public class SitMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("����I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SitName);
        if (stateMachine._sitScripts != null && stateMachine._avatorTrams != null)
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.SitDownPosition();
            stateMachine._avatorTrams.rotation = Quaternion.Euler(stateMachine._sitScripts.SitDownRotation());
        }
        DebugLogUtility.PrankLog("���邩���B", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SitName + "End");
        if (stateMachine._sitScripts != null && stateMachine._avatorTrams != null)
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.StandUp();
        }
        DebugLogUtility.PrankLog("������", stateMachine.DisplayLog);
    }
}
