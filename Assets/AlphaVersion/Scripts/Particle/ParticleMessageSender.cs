using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// パーティクル生成のメッセージを送る処理のラッパー
    /// </summary>
    public static class ParticleMessageSender
    {
        public static void SendMessage(ParticleType type, Vector3 position, Transform parent = null)
        {
            MessageBroker.Default.Publish(new ParticleMessage() 
            { 
                Type = type,
                Position = position,
                Parent = parent,
            });
        }
    }
}
