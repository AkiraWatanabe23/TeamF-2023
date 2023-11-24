using StateMachine;

public class WaitState : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�ҋ@�I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? "" : "", stateMachine.DisplayLog);
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
