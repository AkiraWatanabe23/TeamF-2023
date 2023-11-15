using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMotion : IState
{
    public void InitialState()
    {
        DebugLogUtility.PrankLog("ダンス！準備完了！");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("小梅太夫フィーバータイム！");
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("ちゃんちゃかちゃんちゃんちゃちゃんちゃちゃんちゃん");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("チックショー！");
    }
}
