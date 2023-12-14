using StateMachine;

public class WaitState : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("待機！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("私まーつーわ、いつまでも", stateMachine.DisplayLog);

    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("ひとつ！ふたつ！みっつ！よっつ！いつつ！)┳---バーン...!!---┳(", stateMachine.DisplayLog);
    }
}
