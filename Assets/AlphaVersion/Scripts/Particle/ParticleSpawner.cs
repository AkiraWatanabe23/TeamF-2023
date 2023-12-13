using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// メッセージの受信でパーティクルを生成する機能のクラス
    /// </summary>
    public class ParticleSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ParticleType Type;
            public Particle Prefab; // 本来ならプーリングするためのプール
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
            // 使い終わったプールのDispose
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
        /// Receiverがメッセージを受信した際の処理
        /// メッセージの情報を元にパーティクルをプールから取り出す。
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