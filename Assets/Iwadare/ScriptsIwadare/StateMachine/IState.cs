using StateMachine;
using System;


public interface IState
{
    public void InitialState(StateMachineController stateMachine);
    public void OnEnterState(StateMachineController stateMachine);
    public void OnUpdate(StateMachineController stateMachine);
    public void OnExitState(StateMachineController stateMachine);
}
