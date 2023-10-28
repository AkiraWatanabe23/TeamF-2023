using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionScript : IState
{
    public void InitialState(StateMachineController stateMachine)
    {
        Debug.Log("�G���[�V�����I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("��...�킠�I");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("��...��...�킠�I...��...��...");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("�т�����V...���Ă��Ƃ��I�H");
    }

}
