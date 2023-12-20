using StateMachine;

public class WaitState : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�ҋ@�I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�ҋ@������", stateMachine.DisplayLog);

    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�ҋ@�I���I", stateMachine.DisplayLog);
    }
}
