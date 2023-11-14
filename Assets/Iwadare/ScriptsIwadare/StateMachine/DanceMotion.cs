using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMotion : IState
{
    public void InitialState()
    {
        DebugLogUtility.PrankLog("�_���X�I���������I");
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("���~���v�t�B�[�o�[�^�C���I");
        stateMachine.Anim.Play(stateMachine.DanceName);
    }


    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("����񂿂Ⴉ����񂿂�񂿂Ⴟ��񂿂Ⴟ��񂿂��");
    }

    public void OnExitState(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog("�`�b�N�V���[�I");
    }
}
