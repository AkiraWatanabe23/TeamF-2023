using StateMachine;

public class DanceMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�_���X�I���������I", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("���~���v�t�B�[�o�[�^�C���I", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�`�b�N�V���[�I", stateMachine.DisplayLog);
    }
}
