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
        [SerializeField] ActorSettingsSO _female;
        [SerializeField] ActorSettingsSO _femaleOnkou;
        [SerializeField] ActorSettingsSO _femaleTanki;
        [SerializeField] ActorSettingsSO _male;
        [SerializeField] ActorSettingsSO _maleOnkou;
        [SerializeField] ActorSettingsSO _maleTanki;
        [SerializeField] ActorSettingsSO _robber;

        /// <summary>
        /// スコアのテーブルを参照し、増減するスコアの値を返す
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            ActorSettingsSO so = null;
            if (msg.Key == ScoreKey.Female) { so = _female; }
            if (msg.Key == ScoreKey.FemaleOnkou) so = _femaleOnkou;
            if (msg.Key == ScoreKey.FemaleTanki) so = _femaleTanki;
            if (msg.Key == ScoreKey.Male) { so = _male; }
            if (msg.Key == ScoreKey.MaleOnkou) so = _maleOnkou;
            if (msg.Key == ScoreKey.MaleTanki) so = _maleTanki;
            if (msg.Key == ScoreKey.Robber) { so = _robber; }

            if (so == null) { return -1; }

            float add = msg.Result == EventResult.Success ? so.ActorParamsSet.IncreaseScore : 
                                                            -so.ActorParamsSet.DecreaseScore;
            //// スコアの倍率、フィーバータイムかどうかで変わる
            float scoreRate = msg.State == EventState.Normal ? 1 : so.ActorParamsSet.FeverScoreRate;

            if (msg.Result == EventResult.Success) add *= scoreRate;

            return (int)add;
        }
    }
}