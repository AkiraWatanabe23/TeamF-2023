using StateMachine;
using System;

[Serializable]
public class WalkMotion : IState
{
    //MotionState _state = MotionState.Walk;
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("歩く！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName + "Start");
        DebugLogUtility.PrankLog("歩きはじめるぜ！", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }


    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName + "End");
        DebugLogUtility.PrankLog("歩き終わったぜ...", stateMachine.DisplayLog);
    }
}
