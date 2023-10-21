using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitMotion : IState
{
    public void InitialState(StateMachineController stateMachine)
    {
        throw new System.NotImplementedException();
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("いいかいいか？今から座るぞ？今後悔しても遅いゾ？");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("す、座ったぁ！？こいつぁ座りやがった！");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("お立ち台");
    }
}
