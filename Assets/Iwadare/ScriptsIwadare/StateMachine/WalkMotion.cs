using StateMachine;
using System;

[Serializable]
public class WalkMotion : IState
{
    //MotionState _state = MotionState.Walk;
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�����I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName + "Start");
        DebugLogUtility.PrankLog("�����͂��߂邺�I", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }


    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.WalkName + "End");
        DebugLogUtility.PrankLog("�����I�������...", stateMachine.DisplayLog);
    }
}
