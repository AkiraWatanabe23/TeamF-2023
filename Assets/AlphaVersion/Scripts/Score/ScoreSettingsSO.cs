using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            [Header("成功した際の加算量")]
            public int SuccessBonus = BaseActionScore;
            [Header("失敗した際の減少量")]
            public int FailurePenalty = BaseActionScore;
        }

        [Header("強盗")]
        [SerializeField] ActorScore _robber;
        [Header("客(男)")]
        [SerializeField] ActorScore _male;
        [Header("客(女)")]
        [SerializeField] ActorScore _female;
        [Header("客(ムキムキ)")]
        [SerializeField] ActorScore _muscle;
        [Header("通常時の倍率")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("フィーバータイムの倍率")]
        [SerializeField] float _feverScoreRate = 2;

        public ActorScore Robber => _robber;
        public ActorScore Male => _male;
        public ActorScore Female => _female;
        public ActorScore Muscle => _muscle;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
