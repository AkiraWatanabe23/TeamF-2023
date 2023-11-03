using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���b�Z�[�W�̎�M�Ńp�[�e�B�N���𐶐�����@�\�̃N���X
    /// </summary>
    public class ParticleSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ParticleType Type;
            public Particle Prefab; // �{���Ȃ�v�[�����O���邽�߂̃v�[��
        }

        [SerializeField] ParticleMessageReceiver _messageReceiver;
        [SerializeField] Data[] _data;

        Dictionary<ParticleType, Particle> _dict = new();

        void Awake()
        {
            _dict = _data.ToDictionary(d => d.Type, d => d.Prefab);
        }

        void OnEnable()
        {
            _messageReceiver.OnMessageReceived += OnMessageReceived;
        }

        void OnDisable()
        {
            _messageReceiver.OnMessageReceived -= OnMessageReceived;
        }

        /// <summary>
        /// Receiver�����b�Z�[�W����M�����ۂ̏���
        /// ���b�Z�[�W�̏������Ƀp�[�e�B�N�����v�[��������o���B
        /// </summary>
        void OnMessageReceived(ParticleMessage msg)
        {
            // TODO:���������A�{���Ȃ�v�[�����O���Ă���������o��
            Particle prefab = Instantiate(_dict[msg.Type], msg.Parent);
            prefab.transform.position = msg.Position;
            prefab.transform.rotation = Quaternion.identity;
        }
    }
}