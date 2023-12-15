using StateMachine;

public class AttackMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�U���I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�}�V���K�����U�I���˗��˗��˂������I", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.AttackAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.AttackAniName + "End");
        DebugLogUtility.PrankLog("���߁A�����̒e�ۂ��o�[�e���_�[�ɓ�����B�u���A�����ĂȂ��I�H�v�ނ͕s���g�ł������B", stateMachine.DisplayLog);
    }
}
