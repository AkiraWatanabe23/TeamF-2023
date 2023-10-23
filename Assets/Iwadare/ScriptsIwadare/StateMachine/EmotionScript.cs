using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionScript : IState
{
    public void InitialState(StateMachineController stateMachine)
    {
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("わ...わあ！");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("わ...わ...わあ！...わ...わ...");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("びっくり仰天...ってことぉ！？");
    }

}
