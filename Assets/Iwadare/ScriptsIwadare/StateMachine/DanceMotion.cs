using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMotion : IState
{
    public void InitialState()
    {
        Debug.Log("ダンス！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("小梅太夫フィーバータイム！");
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("ちゃんちゃかちゃんちゃんちゃちゃんちゃちゃんちゃん");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("チックショー！");
    }
}
