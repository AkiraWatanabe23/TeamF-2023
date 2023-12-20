using StateMachine;

public class SuccessMotionScript : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�����G���[�V�����I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SuccessName + "Start");
        DebugLogUtility.PrankLog("�����̉����I", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {

        stateMachine.Anim.Play(stateMachine.SuccessName + "End");
        DebugLogUtility.PrankLog("�����I���", stateMachine.DisplayLog);
    }

}
