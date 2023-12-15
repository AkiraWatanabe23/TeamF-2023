using StateMachine;

public class BanditHitsMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("Banditヒット！準備完了！",disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.HitsAniName);
        DebugLogUtility.PrankLog("やはり西部か...いつ出発する？私はもう帰る。",stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }


    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("臆病バンディ院",stateMachine.DisplayLog);
    }
}
