using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    using ActorScore = ScoreSettings.ActorScore;
    using static ScoreEventMessage;

    /// <summary>
    /// スコアの増減のメッセージから増減する値を計算する機能のクラス
    /// </summary>
    public class ScoreCalculator : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;

        /// <summary>
        /// スコアのテーブルを参照し、増減するスコアの値を返す
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            ScoreSettings settings = _settings.ScoreSettings;
            
            // スコアの倍率、フィーバータイムかどうかで変わる
            float scoreRate = msg.State == EventState.Normal ? settings.DefaultScoreRate : settings.FeverScoreRate;

            // イベントを起こしたキャラクター
            ActorScore actor = default;
            if      (msg.Actor == EventActor.Male) actor = settings.Male;
            else if (msg.Actor == EventActor.Female) actor = settings.Female;
            else if (msg.Actor == EventActor.Muscle) actor = settings.Muscle;
            else throw new System.Exception("スコアを送信したキャラが想定外: " + actor);

            // 成功/失敗で増減する値の基準値を決める
            float score = msg.Result == EventResult.Success ? actor.SuccessBonus : -actor.FailurePenalty;

            // 成功の場合はスコア倍率をかける
            if (msg.Result == EventResult.Success) score *= scoreRate;

            return (int)score;
        }
    }
}
