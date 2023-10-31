using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �I�������A�C�e���̃��b�Z�[�W����M���A�\������UI�̃N���X
    /// </summary>
    public class ItemSelectUI : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public Sprite Icon;
        }

        [SerializeField] ItemMessageReceiver _messageReceiver;
        [Header("���ɍ�����E�ɕ\�������")]
        [SerializeField] Data[] _data;
        [SerializeField] ItemFrameUI[] _frames;

        /// <summary>
        /// �A�C�e���̎�ނɑΉ�����UI�̎���
        /// </summary>
        Dictionary<ItemType, ItemFrameUI> _dict = new();

        void OnEnable()
        {
            _messageReceiver.OnMessageReceived += OnMessageReceived;
        }

        void OnDisable()
        {
            _messageReceiver.OnMessageReceived -= OnMessageReceived;
        }

        void Start()
        {
            ApplyDataToFrame();
        }

        /// <summary>
        /// �A�C�e���̃A�C�R���̃f�[�^�̐������t���[����L��������
        /// </summary>
        void ApplyDataToFrame()
        {
            for (int i = 0; i < _frames.Length; i++)
            {
                if (i < _data.Length)
                {
                    _frames[i].Valid(_data[i].Icon);

                    // �A�C�e���őΉ�����UI���擾�ł���悤�Ɏ����ɒǉ�
                    _dict.Add(_data[i].Type, _frames[i]);
                }
                else
                {
                    _frames[i].InValid();
                }
            }
        }

        /// <summary>
        /// Receiver�����b�Z�[�W����M�����ۂ̏���
        /// �I�������A�C�e���ɑΉ�����UI��I������
        /// </summary>
        void OnMessageReceived(ItemSelectMessage msg)
        {
            // ��U���ׂĖ�����
            foreach (ItemFrameUI frame in _frames)
            {
                frame.Deselect();
            }

            // �I�������A�C�e����L����
            _dict[msg.Type].Select();
        }
    }
}
