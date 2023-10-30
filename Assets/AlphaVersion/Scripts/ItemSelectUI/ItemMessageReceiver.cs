using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// 選択したアイテムをメッセージングするための構造体
    /// </summary>
    public struct ItemSelectMessage
    {
        public ItemType Type;
    }

    /// <summary>
    /// アイテムを選択した際に送信されるメッセージを受信するクラス
    /// </summary>
    public class ItemMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// メッセージを受信した際に呼ばれる処理
        /// </summary>
        public event UnityAction<ItemSelectMessage> OnMessageReceived;

        void Awake()
        {
            MessageBroker.Default.Receive<ItemSelectMessage>().Subscribe(msg =>
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
