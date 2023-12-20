using StateMachine;

public class DanceMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("ダンス！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("ダンスダンスフィーバータイム！", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("ダンス終わって冷めたチキン。", stateMachine.DisplayLog);
    }
}
