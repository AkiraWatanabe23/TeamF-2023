using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �������ʂɉ������X�R�A�𑗐M����@�\�̃N���X
    /// </summary>
    public static class OrderScoreSender
    {
        /// <summary>
        /// �X�R�A�𑗐M����
        /// </summary>
        public static void SendScore(OrderResult result, ActorType actor, Tension tension, Vector3 position)
        {
            if (result == OrderResult.Success)
            {
                if (tension == Tension.Normal) ScoreMessageSender.SendSuccessMessage(actor, position);
                if (tension == Tension.Ferver) ScoreMessageSender.SendFeverSuccessMessage(actor, position);
            }
            else if (result == OrderResult.Failure)
            {
                // �t�B�[�o�[���̓X�R�A���������Ȃ��̂Œʏ펞�̂ݑ��M����
                if (tension == Tension.Normal) ScoreMessageSender.SendFailureMessage(actor, position);
            }
        }
    }
}
