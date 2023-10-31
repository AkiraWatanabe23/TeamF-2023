using StateMachine;
using System;
using UnityEngine;


public interface IState
{
    public void InitialState();
    public void OnEnterState(StateMachineController stateMachine);
    public void OnUpdate(StateMachineController stateMachine);
    public void OnExitState(StateMachineController stateMachine);
}
