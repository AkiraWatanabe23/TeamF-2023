using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace Alpha
{
    /// <summary>
    /// インゲームの設定
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        // スコアによる評価の設定
        [System.Serializable]
        public class ScoreEvaluate
        {
            public int Score;
            public string Evaluate;
        }

        [Header("<color=#00FF76>制限時間</color>")]
        [SerializeField] int _timeLimit = 60;
        [Header("<color=#00FF76>フィーバータイム開始</color>")]
        [SerializeField] int _ferverTime = 20;
        [Header("<color=#00FF76>スコアに応じた評価</color>")]
        [SerializeField] ScoreEvaluate[] _evaluate;

        public float TimeLimit => _timeLimit;
        public int FerverTime => _ferverTime;

        /// <summary>
        /// スコアに応じた評価を返す
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public string GetEvaluate(int score)
        {
            foreach (ScoreEvaluate eval in _evaluate.OrderByDescending(e => e.Score))
            {
                if (score >= eval.Score) return eval.Evaluate;
            }

            return "存在しない評価";
        }
    }
}
