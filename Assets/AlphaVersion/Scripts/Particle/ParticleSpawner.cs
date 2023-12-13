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

        Dictionary<ParticleType, ParticlePool> _pools;

        void Awake()
        {
            _pools = _data.ToDictionary(d => d.Type, d => new ParticlePool(d.Prefab, $"ParticlePool_{d.Type}"));
        }

        void OnDestroy()
        {
            // �g���I������v�[����Dispose
            foreach (KeyValuePair<ParticleType, ParticlePool> pair in _pools) pair.Value.Dispose();
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
            Particle particle = _pools[msg.Type].Rent();
            particle.Play();
            particle.transform.parent = msg.Parent;
            particle.transform.position = msg.Position;
            particle.transform.rotation = Quaternion.identity;
        }
    }
}