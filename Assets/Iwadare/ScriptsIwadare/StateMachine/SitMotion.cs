using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitMotion : IState
{
    public void InitialState(StateMachineController stateMachine)
    {
        Debug.Log("����I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("�������������H��������邼�H��������Ă��x���]�H");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("���A���������I�H����������₪�����I");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("��������");
    }
}
