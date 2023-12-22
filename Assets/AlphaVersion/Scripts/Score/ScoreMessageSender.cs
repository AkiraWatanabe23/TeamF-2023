using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    using static ScoreEventMessage;

    /// <summary>
    /// ���b�Z�[�W�𑗐M���鏈���̃��b�p�[
    /// </summary>
    public static class ScoreMessageSender
    {
        /// <summary>
        /// �ʏ펞�A���������ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendSuccessMessage(ActorType actor, Vector3 pos, ScoreKey key)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Key = key,
                Result = EventResult.Success,
                State = EventState.Normal,
                Actor = Convert(actor),
                Position = pos,
            });
        }

        /// <summary>
        /// �t�B�[�o�[���A���������ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendFeverSuccessMessage(ActorType actor, Vector3 pos, ScoreKey key)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Key = key,
                Result = EventResult.Success,
                State = EventState.Ferver,
                Actor = Convert(actor),
                Position = pos,
            });
        }

        /// <summary>
        /// �ʏ펞�A���s�����ۂ̃��b�Z�[�W�𑗐M����
        /// </summary>
        public static void SendFailureMessage(ActorType actor, Vector3 pos, ScoreKey key)
        {
            MessageBroker.Default.Publish(new ScoreEventMessage()
            {
                Key = key,
                Result = EventResult.Failure,
                State = EventState.Normal,
                Actor = Convert(actor),
                Position = pos,
            });
        }

        /// <summary>
        /// ���b�Z�[�W�p��Actor�ɕϊ�����
        /// </summary>
        static EventActor Convert(ActorType actor)
        {
            if (actor == ActorType.Male) return EventActor.Male;
            if (actor == ActorType.Female) return EventActor.Female;
            if (actor == ActorType.Muscle) return EventActor.Muscle;

            throw new System.ArgumentException("�X�R�A�̃��b�Z�[�W���O��ActorType���Ή����Ă��Ȃ�: " + actor);
        }
    }
}
