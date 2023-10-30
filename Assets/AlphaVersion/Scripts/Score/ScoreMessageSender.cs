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
    /// メッセージを送信する処理のラッパー
    /// </summary>
    public static class ScoreMessageSender
    {
        /// <summary>
        /// 通常時、成功した際のメッセージを送信する
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
        /// フィーバー時、成功した際のメッセージを送信する
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
        /// 通常時、失敗した際のメッセージを送信する
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
        /// メッセージ用のActorに変換する
        /// </summary>
        static ScoreEventMessage.EventActor Convert(ActorType actor)
        {
            if (actor == ActorType.Male) return EventActor.Male;
            if (actor == ActorType.Female) return EventActor.Female;
            if (actor == ActorType.Muscle) return EventActor.Muscle;

            throw new System.ArgumentException("スコアのメッセージングにActorTypeが対応していない: " + actor);
        }
    }
}
