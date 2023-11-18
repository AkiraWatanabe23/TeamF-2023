using StateMachine;


public interface IState
{
    public void InitialState(bool disDebugLog);
    public void OnEnterState(StateMachineController stateMachine);
    public void OnUpdate(StateMachineController stateMachine);
    public void OnExitState(StateMachineController stateMachine);
}
