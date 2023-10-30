using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �I�������A�C�e�������b�Z�[�W���O���邽�߂̍\����
    /// </summary>
    public struct ItemSelectMessage
    {
        public ItemType Type;
    }

    /// <summary>
    /// �A�C�e����I�������ۂɑ��M����郁�b�Z�[�W����M����N���X
    /// </summary>
    public class ItemMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// ���b�Z�[�W����M�����ۂɌĂ΂�鏈��
        /// </summary>
        public event UnityAction<ItemSelectMessage> OnMessageReceived;

        void Awake()
        {
            MessageBroker.Default.Receive<ItemSelectMessage>().Subscribe(msg =>
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
