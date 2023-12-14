using StateMachine;

public class FailedMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("失敗エモーション！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.FailedName);
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "ぷん男くん！肉を切ってるこの刀は何？はあ、なるほど" :
            "名刀ぷんぷん丸！！", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "チンゲン菜を切って、この刀の切れ味、いかがなものか試してみるね！" :
            "つまら怒ものを斬ってしまった...", stateMachine.DisplayLog);
    }

}
