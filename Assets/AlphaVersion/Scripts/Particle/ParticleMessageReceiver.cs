using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// 生成するパーティクルの情報をメッセージングするための構造体
    /// </summary>
    public struct ParticleMessage
    {
        public ParticleType Type;
        public Vector3 Position;
        public Transform Parent;
    }

    /// <summary>
    /// パーティクルを生成する際に送信されるメッセージを受信するクラス
    /// </summary>
    public class ParticleMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// メッセージを受信した際に呼ばれる処理
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
