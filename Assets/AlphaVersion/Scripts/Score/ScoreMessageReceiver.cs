using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// ���������X�R�A�𑝌�������C�x���g�̏������b�Z�[�W���O���邽�߂̍\����
    /// </summary>
    public struct ScoreEventMessage
    {
        public enum EventResult
        {
            Success,
            Failure,
        }

        public enum EventState
        {
            Normal,
            Ferver,
        }

        public enum EventActor
        {
            Male,
            Female,
            Muscle,
        }

        public EventResult Result;
        public EventState State;
        public EventActor Actor;
    }

    /// <summary>
    /// �e�N���X���瑗�M�����A�X�R�A�̑����̃��b�Z�[�W����M����N���X
    /// </summary>
    public class ScoreMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// ���b�Z�[�W����M�����ۂɌĂ΂�鏈��
        /// </summary>
        public event UnityAction<ScoreEventMessage> OnMessageReceived;

        void Awake()
        {
            // ��M�����ۂɃR�[���o�b�N���Ăт����B
            // ���̃��C���[�Ŏ�M�����C�x���g�ɂ���ĉ��o���s�������ꍇ���l�����āA
            // �����Ő��l�ɕϊ������A���b�Z�[�W�^�̂܂ܗ����B
            MessageBroker.Default.Receive<ScoreEventMessage>().Subscribe(msg =>
            {
                OnMessageReceived?.Invoke(msg);
            }).AddTo(gameObject);
        }

        void OnDestroy()
        {
            OnMessageReceived = null;
        }
    }
}