using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMotion : IState
{
    public void InitialState()
    {
        Debug.Log("�_���X�I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        Debug.Log("���~���v�t�B�[�o�[�^�C���I");
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        Debug.Log("����񂿂Ⴉ����񂿂�񂿂Ⴟ��񂿂Ⴟ��񂿂��");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        Debug.Log("�`�b�N�V���[�I");
    }
}
