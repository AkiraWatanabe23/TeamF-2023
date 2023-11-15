using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SitMotion : IState
{
    public void InitialState()
    {
        DebugLogUtility.PrankLog("座る！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SitName);
        if(stateMachine._sitScripts != null && stateMachine._avatorTrams != null) 
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.SitDownPosition();
            stateMachine._avatorTrams.rotation = Quaternion.Euler(stateMachine._sitScripts.SitDownRotation());
        }
        DebugLogUtility.PrankLog("いいかいいか？今から座るぞ？今後悔しても遅いゾ？");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("す、座ったぁ！？こいつぁ座りやがった！");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        if (stateMachine._sitScripts != null && stateMachine._avatorTrams != null)
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.StandUp();
        }
        DebugLogUtility.PrankLog("お立ち台");
    }
}
