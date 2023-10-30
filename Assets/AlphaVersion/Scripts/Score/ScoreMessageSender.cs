using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EventResult = Alpha.ScoreEventMessage.EventResult;
using EventState = Alpha.ScoreEventMessage.EventState;
using EventActor = Alpha.ScoreEventMessage.EventActor;

namespace Alpha
{
    /// <summary>
    /// ���b�Z�[�W�𑗐M���鏈���̃��b�p�[
    /// </summary>
    public static class ScoreMessageSender
    {
        /// <summary>
        /// �ʏ펞�A���������ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendSuccessMessage(ActorType actor)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Result = EventResult.Success,
                State = EventState.Normal,
                Actor = Convert(actor),
            });
        }

        /// <summary>
        /// �t�B�[�o�[���A���������ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendFeverSuccessMessage(ActorType actor)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Result = EventResult.Success,
                State = EventState.Ferver,
                Actor = Convert(actor),
            });
        }

        /// <summary>
        /// �ʏ펞�A���s�����ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendFailureMessage(ActorType actor)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Result = EventResult.Failure,
                State = EventState.Normal,
                Actor = Convert(actor),
            });
        }

        /// <summary>
        /// ���b�Z�[�W�p��Actor�ɕϊ�����
        /// </summary>
        static ScoreEventMessage.EventActor Convert(ActorType actor)
        {
            if (actor == ActorType.Male) return EventActor.Male;
            if (actor == ActorType.Female) return EventActor.Female;
            if (actor == ActorType.Muscle) return EventActor.Muscle;

            throw new System.ArgumentException("�X�R�A�̃��b�Z�[�W���O��ActorType���Ή����Ă��Ȃ�: " + actor);
        }
    }
}
