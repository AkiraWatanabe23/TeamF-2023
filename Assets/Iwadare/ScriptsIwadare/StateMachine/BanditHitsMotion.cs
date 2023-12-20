using StateMachine;

public class BanditHitsMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("Bandit�q�b�g�I���������I",disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.HitsAniName);
        DebugLogUtility.PrankLog("Bandit��Hits�I",stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }


    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("Hits�I���IBandit�ܖڂ��I",stateMachine.DisplayLog);
    }
}
