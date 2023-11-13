using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessMotionScript : IState
{
    public void InitialState()
    {
        Debug.Log("成功エモーション！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SuccessName);
        Debug.Log(stateMachine.NGWordbool ? 
            "おっ、何かいいものがもらえたぞ！" : 
            "これはあちらのお客様からです。");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log(stateMachine.NGWordbool ? 
            "あっ、なるほど！" :
            "素晴らしいものをお持ちで。");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log(stateMachine.NGWordbool ? 
            "栗とリスの石像とワインがもらえたのか！これは嬉しいな！" : 
            "また来ます。");
    }

}
