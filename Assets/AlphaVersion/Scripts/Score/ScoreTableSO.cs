using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// スコアの計算に関する値の設定
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreTableSO", menuName = "ScoreData")]
    public class ScoreTableSO : ScriptableObject
    {
        /// <summary>
        /// 各アクションの増減の基準となるスコアの値
        /// </summary>
        const int BaseActionScore = 100;

        /// <summary>
        /// 客・強盗の基底クラス
        /// </summary>
        [System.Serializable]
        public class Actor
        {
            [Header("成功した際の加算量")]
            [SerializeField] int SuccessBonus = BaseActionScore;
            [Header("失敗した際の減少量")]
            [SerializeField] int FailurePenalty = BaseActionScore;
        }

        [Header("強盗")]
        [SerializeField] Actor _robber;
        [Header("客(男)")]
        [SerializeField] Actor _male;
        [Header("客(女)")]
        [SerializeField] Actor _female;
        [Header("客(ムキムキ)")]
        [SerializeField] Actor _muscle;
        [Header("通常時の倍率")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("フィーバータイムの倍率")]
        [SerializeField] float _feverScoreRate = 2;

        public Actor Robber => _robber;
        public Actor Male => _male;
        public Actor Female => _female;
        public Actor Muscle => _muscle;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
