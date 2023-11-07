using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessMotionScript : IState
{
    public void InitialState()
    {
        Debug.Log("�����G���[�V�����I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SuccessName);
        Debug.Log(stateMachine._nGWordbool ? 
            "�����A�����������̂����炦�����I" : 
            "����͂�����̂��q�l����ł��B");
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log(stateMachine._nGWordbool ? 
            "�����A�Ȃ�قǁI" :
            "�f���炵�����̂��������ŁB");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log(stateMachine._nGWordbool ? 
            "�I�ƃ��X�̐Α��ƃ��C�������炦���̂��I����͊������ȁI" : 
            "�܂����܂��B");
    }

}
