using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// ��������p�[�e�B�N���̏������b�Z�[�W���O���邽�߂̍\����
    /// </summary>
    public struct ParticleMessage
    {
        public ParticleType Type;
        public Vector3 Position;
        public Transform Parent;
    }

    /// <summary>
    /// �p�[�e�B�N���𐶐�����ۂɑ��M����郁�b�Z�[�W����M����N���X
    /// </summary>
    public class ParticleMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// ���b�Z�[�W����M�����ۂɌĂ΂�鏈��
        /// </summary>
        public event UnityAction<ParticleMessage> OnMessageReceived;

        void Awake()
        {
            MessageBroker.Default.Receive<ParticleMessage>().Subscribe(msg =>
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
