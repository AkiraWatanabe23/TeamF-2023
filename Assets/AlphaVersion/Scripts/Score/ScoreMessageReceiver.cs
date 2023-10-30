using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// 発生したスコアを増減させるイベントの情報をメッセージングするための構造体
    /// </summary>
    public struct ScoreEventMessage
    {
        public enum EventResult
        {
            Success,
            Failure,
        }

        public enum EventState
        {
            Normal,
            Ferver,
        }

        public enum EventActor
        {
            Male,
            Female,
            Muscle,
        }

        public EventResult Result;
        public EventState State;
        public EventActor Actor;
    }

    /// <summary>
    /// 各クラスから送信される、スコアの増減のメッセージを受信するクラス
    /// </summary>
    public class ScoreMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// メッセージを受信した際に呼ばれる処理
        /// </summary>
        public event UnityAction<ScoreEventMessage> OnMessageReceived;

        void Awake()
        {
            // 受信した際にコールバックを呼びだす。
            // 下のレイヤーで受信したイベントによって演出を行いたい場合を考慮して、
            // ここで数値に変換せず、メッセージ型のまま流す。
            MessageBroker.Default.Receive<ScoreEventMessage>().Subscribe(msg =>
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