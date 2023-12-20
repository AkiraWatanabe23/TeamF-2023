using StateMachine;

public class AttackMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�U���I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�U���I�}�V���K���𔭎˂���I", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.AttackAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.AttackAniName + "End");
        DebugLogUtility.PrankLog("�U���I���I�o�[�e���_�[�͑S�Ă̒e���]���B", stateMachine.DisplayLog);
    }
}
