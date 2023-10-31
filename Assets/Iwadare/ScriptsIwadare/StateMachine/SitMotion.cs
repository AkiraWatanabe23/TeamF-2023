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
        Debug.Log("座る！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SitName);
        if(stateMachine._sitScripts != null && stateMachine._avatorTrams != null) 
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.SitDownPosition();
            stateMachine._avatorTrams.rotation = Quaternion.Euler(stateMachine._sitScripts.SitDownRotation());
        }
        Debug.Log("いいかいいか？今から座るぞ？今後悔しても遅いゾ？");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("す、座ったぁ！？こいつぁ座りやがった！");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        if (stateMachine._sitScripts != null && stateMachine._avatorTrams != null)
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.StandUp();
        }
        Debug.Log("お立ち台");
    }
}
