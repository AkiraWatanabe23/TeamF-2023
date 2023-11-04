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
        /// Receiverがメッセージを受信した際の処理
        /// メッセージの情報を元にパーティクルをプールから取り出す。
        /// </summary>
        void OnMessageReceived(ParticleMessage msg)
        {
            // TODO:生成処理、本来ならプーリングしてそこから取り出す
            Particle prefab = Instantiate(_dict[msg.Type], msg.Parent);
            prefab.transform.position = msg.Position;
            prefab.transform.rotation = Quaternion.identity;
        }
    }
}