using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessMotionScript : IState
{
    public void InitialState(bool disDebugLog)
    {
        DebugLogUtility.PrankLog("�����G���[�V�����I���������I",disDebugLog);
    }

    public void OnEnterState(StateMachineController stateMachine)
    {
        stateMachine.Anim.Play(stateMachine.SuccessName + "Start");
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? 
            "�����A�����������̂����炦�����I" : 
            "����͂�����̂��q�l����ł��B", stateMachine.DisplayLog);
    }

    public void OnUpdate(StateMachineController stateMachine)
    {
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? 
            "�����A�Ȃ�قǁI" :
            "�f���炵�����̂��������ŁB", stateMachine.DisplayLog);
    }

    public void OnExitState(StateMachineController stateMachine)
    {

        stateMachine.Anim.Play(stateMachine.SuccessName + "End");
        DebugLogUtility.PrankLog(stateMachine.NGWordbool ? 
            "�I�ƃ��X�̐Α��ƃ��C�������炦���̂��I����͊������ȁI" : 
            "�܂����܂��B", stateMachine.DisplayLog);
    }

}
