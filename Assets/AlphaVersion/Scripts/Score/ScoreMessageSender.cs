using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    using static ScoreEventMessage;

    /// <summary>
    /// メッセージを送信する処理のラッパー
    /// </summary>
    public static class ScoreMessageSender
    {
        /// <summary>
        /// 通常時、成功した際のメッセージを送信する
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
        /// フィーバー時、成功した際のメッセージを送信する
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
        /// 通常時、失敗した際のメッセージを送信する
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
        /// メッセージ用のActorに変換する
        /// </summary>
        static EventActor Convert(ActorType actor)
        {
            if (actor == ActorType.Male) return EventActor.Male;
            if (actor == ActorType.Female) return EventActor.Female;
            if (actor == ActorType.Muscle) return EventActor.Muscle;

            throw new System.ArgumentException("スコアのメッセージングにActorTypeが対応していない: " + actor);
        }
    }
}
