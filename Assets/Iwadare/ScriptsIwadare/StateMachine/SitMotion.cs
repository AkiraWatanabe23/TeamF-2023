using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SitMotion : IState
{
    public void InitialState()
    {
        Debug.Log("����I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SitName);
        if(stateMachine._sitScripts != null && stateMachine._avatorTrams != null) 
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.SitDownPosition();
            stateMachine._avatorTrams.rotation = Quaternion.Euler(stateMachine._sitScripts.SitDownRotation());
        }
        Debug.Log("�������������H��������邼�H��������Ă��x���]�H");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("���A���������I�H����������₪�����I");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        if (stateMachine._sitScripts != null && stateMachine._avatorTrams != null)
        {
            stateMachine._avatorTrams.position = stateMachine._sitScripts.StandUp();
        }
        Debug.Log("��������");
    }
}
