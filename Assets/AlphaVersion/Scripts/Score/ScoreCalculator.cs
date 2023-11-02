using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    using static ScoreEventMessage;

    /// <summary>
    /// スコアの増減のメッセージから増減する値を計算する機能のクラス
    /// </summary>
    public class ScoreCalculator : MonoBehaviour
    {
        [SerializeField] ScoreTableSO _table;

        /// <summary>
        /// スコアのテーブルを参照し、増減するスコアの値を返す
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            // スコアの倍率、フィーバータイムかどうかで変わる
            float scoreRate = msg.State == EventState.Normal ? _table.DefaultScoreRate : _table.FeverScoreRate;

            // イベントを起こしたキャラクター
            ScoreTableSO.Actor actor = msg.Actor == EventActor.Male ? _table.Male :
                                       msg.Actor == EventActor.Female ? _table.Female :
                                                                        _table.Muscle;
            // 成功/失敗で増減する値の基準値を決める
            float score = msg.Result == EventResult.Success ? actor.SuccessBonus : -actor.FailurePenalty;

            // 成功の場合はスコア倍率をかける
            if (msg.Result == EventResult.Success) score *= scoreRate;

            return (int)score;
        }
    }
}
