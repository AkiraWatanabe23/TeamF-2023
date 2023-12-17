using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// ���O�h�[���̃��b�Z�[�W�̑��M�̃��b�p�[
    /// </summary>
    public static class RagDollMessageSender
    {
        public static void SendMessage(RagDollType type, Transform model, Vector3 hitPosition)
        {
            MessageBroker.Default.Publish(new RagDollMessage() 
            {
                Type = type,
                Model = model,
                HitPosition = hitPosition,
            });
        }
    }
}
