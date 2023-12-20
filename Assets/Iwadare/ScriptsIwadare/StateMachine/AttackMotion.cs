using StateMachine;

public class AttackMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("攻撃！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("攻撃！マシンガンを発射せよ！", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.AttackAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.AttackAniName + "End");
        DebugLogUtility.PrankLog("攻撃終わり！バーテンダーは全ての弾を躱す。", stateMachine.DisplayLog);
    }
}
