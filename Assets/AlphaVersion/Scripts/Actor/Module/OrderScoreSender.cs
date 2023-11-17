using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 注文結果に応じたスコアを送信する機能のクラス
    /// </summary>
    public static class OrderScoreSender
    {
        /// <summary>
        /// スコアを送信する
        /// </summary>
        public static void SendScore(OrderResult result, ActorType actor, Tension tension)
        {
            if (result == OrderResult.Success)
            {
                if (tension == Tension.Normal) ScoreMessageSender.SendSuccessMessage(actor);
                if (tension == Tension.Ferver) ScoreMessageSender.SendFeverSuccessMessage(actor);
            }
            else if (result == OrderResult.Failure)
            {
                // フィーバー時はスコアが減少しないので通常時のみ送信する
                if (tension == Tension.Normal) ScoreMessageSender.SendFailureMessage(actor);
            }
        }
    }
}
