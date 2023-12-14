using StateMachine;

public class SuccessMotionScript : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("成功エモーション！準備完了！", disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SuccessName + "Start");
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "おっ、何かいいものがもらえたぞ！" :
            "これはあちらのお客様からです。", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        return;
    }

    public void OnExitState(StateMachineController stateMachine)
    {

        stateMachine.Anim.Play(stateMachine.SuccessName + "End");
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ?
            "栗とリスの石像とワインがもらえたのか！これは嬉しいな！" :
            "また来ます。", stateMachine.DisplayLog);
    }

}
