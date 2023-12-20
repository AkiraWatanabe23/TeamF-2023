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
        DebugLogUtility.PrankLog("BanditにHits！",stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }


    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("Hits終わり！Bandit涙目だ！",stateMachine.DisplayLog);
    }
}
