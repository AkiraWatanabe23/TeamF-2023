using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// メッセージを送信する処理のラッパー
    /// </summary>
    public static class ItemMessageSender
    {
        public static void SendMessage(ItemType item)
        {
            MessageBroker.Default.Publish(new ItemSelectMessage() { Type = item });
        }
    }
}
