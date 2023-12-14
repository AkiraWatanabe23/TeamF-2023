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
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "�Ղ�j����I����؂��Ă邱�̓��͉��H�͂��A�Ȃ�ق�" :
            "�����Ղ�Ղ�ہI�I", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "�`���Q���؂�؂��āA���̓��̐؂ꖡ�A�������Ȃ��̂������Ă݂�ˁI" :
            "�܂�{���̂��a���Ă��܂���...", stateMachine.DisplayLog);
    }

}
