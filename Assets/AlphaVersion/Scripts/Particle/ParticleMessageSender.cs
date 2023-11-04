using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �p�[�e�B�N�������̃��b�Z�[�W�𑗂鏈���̃��b�p�[
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
