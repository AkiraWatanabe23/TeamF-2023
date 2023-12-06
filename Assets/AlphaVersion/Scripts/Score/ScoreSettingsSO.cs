using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Alpha
{
    /// <summary>
    /// スコアに関する設定
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreSettingsSO", menuName = "Settings/ScoreSettings")]
    [System.Serializable]
    public class ScoreSettingsSO : ScriptableObject
    {
        // 各アクションで増減するスコアの基準値
        const int BaseActionScore = 100;

        // 客・強盗の基底クラス
        [System.Serializable]
        public class ActorScore
        {
            [Header("<color=#00FF76>成功した際の加算量</color>")]
            public int SuccessBonus = BaseActionScore;
            [Header("<color=#00FF76>失敗した際の減少量</color>")]
            public int FailurePenalty = BaseActionScore;
        }

        [Header("<color=#00FF76>強盗</color>")]
        [SerializeField] ActorScore _robber;
        [Header("<color=#00FF76>客(男)</color>")]
        [SerializeField] ActorScore _male;
        [Header("<color=#00FF76>客(女)</color>")]
        [SerializeField] ActorScore _female;
        [Header("<color=#00FF76>通常時の倍率</color>")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("<color=#00FF76>フィーバータイムの倍率</color>")]
        [SerializeField] float _feverScoreRate = 2;

        public ActorScore Robber => _robber;
        public ActorScore Male => _male;
        public ActorScore Female => _female;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
