using StateMachine;

public class AttackMotion : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("攻撃！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("マシンガン装填！乱射乱射乱射あああ！", stateMachine.DisplayLog);
        stateMachine.Anim.Play(stateMachine.AttackAniName);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.AttackAniName + "End");
        DebugLogUtility.PrankLog("刹那、無数の弾丸がバーテンダーに当たる。「き、効いてない！？」彼は不死身であった。", stateMachine.DisplayLog);
    }
}
