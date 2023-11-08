using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// ギミックに関する設定
    /// </summary>
    [System.Serializable]
    public class GimmickSettings
    {
        [SerializeField] float _rate = 100.0f;
        [SerializeField] float _delta = 1.0f;
        [Header("ギミック発生のランダム性")]
        [Range(0, 1.0f)]
        [SerializeField] float _random = 1.0f;

        public float Rate => _rate;
        public float FixedDelta => _delta * Random.Range(1.0f - _random, 1.0f);
    }

    /// <summary>
    /// インゲームの設定
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("制限時間")]
        [SerializeField] float _timeLimit;
        [Header("客の生成間隔")]
        [SerializeField] float _customerSpawnRate = 3.0f;
        [Header("フィーバーに必要なスコア")]
        [SerializeField] int _ferverScoreThreshold = 5000;
        [Header("フィーバーの持続時間(秒)")]
        [SerializeField] float _ferverTimeLimit = 10.0f;
        [Header("タンブルウィードのギミックの設定")]
        [SerializeField] GimmickSettings _tumbleweed;
        [Header("強盗のギミックの設定")]
        [SerializeField] GimmickSettings _robber;

        public float TimeLimit => _timeLimit;
        public float CustomerSpawnRate => _customerSpawnRate;
        public int FerverScoreThreshold => _ferverScoreThreshold;
        public float FerverTimeLimit => _ferverTimeLimit;
        public GimmickSettings TumbleWeed => _tumbleweed;
        public GimmickSettings Robber => _robber;
    }
}
