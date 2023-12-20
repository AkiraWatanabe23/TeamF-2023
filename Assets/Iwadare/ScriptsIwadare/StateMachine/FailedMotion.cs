using StateMachine;

public class FailedMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("���s�G���[�V�����I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.FailedName);
        DebugLogUtility.PrankLog("���s�I�q�͂Ղ�Ղ�I", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("���s�I���I�{���ċA��I", stateMachine.DisplayLog);
    }

}
