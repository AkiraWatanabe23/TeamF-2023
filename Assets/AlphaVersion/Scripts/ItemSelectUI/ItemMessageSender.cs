using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// ���b�Z�[�W�𑗐M���鏈���̃��b�p�[
    /// </summary>
    public static class ItemMessageSender
    {
        public static void SendMessage(ItemType item)
        {
            MessageBroker.Default.Publish(new ItemSelectMessage() { Type = item });
        }
    }
}
