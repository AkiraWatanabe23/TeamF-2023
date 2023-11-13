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
        [Header("ギミック発生のタイミング(秒)")]
        [SerializeField] float[] _timing;
        [Header("タイミングのランダム性")]
        [Range(0, 1.0f)]
        [SerializeField] float _random = 1.0f;

        public IReadOnlyList<float> Timing => _timing ??= new float[0];
        public float FixedDelta => Random.Range(1.0f - _random, 1.0f);
    }

    /// <summary>
    /// キャッチ判定に関する設定
    /// </summary>
    [System.Serializable]
    public class CatchSettings
    {
        [Header("大きさの設定")]
        [Range(0.1f, 0.8f)]
        [SerializeField] float _normalSize = 0.25f;
        [Range(0.1f, 0.8f)]
        [SerializeField] float _ferverSize = 0.8f;
        [Header("商品をキャッチできる速度")]
        [SerializeField] float _catchableSpeed = 1.0f;

        public float NormalSize => _normalSize;
        public float FerverSize => _ferverSize;
        public float CatchableSpeed => _catchableSpeed;
    }

    /// <summary>
    /// スコアに関する設定
    /// </summary>
    [System.Serializable]
    public class ScoreSettings
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

    /// <summary>
    /// 客の生成確率に関する設定
    /// </summary>
    [System.Serializable]
    public class SpawnRateSettings
    {
        [Range(1, 100)]
        [SerializeField] int _maleRate;
        [Range(1, 100)]
        [SerializeField] int _femaleRate;
        [Range(1, 100)]
        [SerializeField] int _muscleRate;

        /// <summary>
        /// 重み付き確率抽選で生成するキャラクターを選ぶ
        /// </summary>
        public ActorType RandomActor
        {
            get
            {
                int max = _maleRate + _femaleRate + _muscleRate;
                int r = Random.Range(1, max + 1);
                
                if (r <= _maleRate) return ActorType.Male;
                else if (r <= _maleRate + _muscleRate) return ActorType.Female;
                else return ActorType.Muscle;
            }
        }
    }

    /// <summary>
    /// インゲームの設定
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("制限時間")]
        [SerializeField] int _timeLimit = 60;
        [Header("フィーバータイム開始")]
        [SerializeField] int _ferverTime = 20;
        [Header("客の生成間隔(秒)")]
        [SerializeField] float _customerSpawnRate = 3.0f;
        [Header("タンブルウィードのギミックの設定")]
        [SerializeField] GimmickSettings _tumbleweed;
        [Header("強盗のギミックの設定")]
        [SerializeField] GimmickSettings _robber;
        [Header("客がキャッチする判定の設定")]
        [SerializeField] CatchSettings _catchSettings;
        [Header("客毎の生成確率設定")]
        [SerializeField] SpawnRateSettings _spawnRateSettings;
        [Header("スコアの設定")]
        [SerializeField] ScoreSettings _scoreSettings;

        public float TimeLimit => _timeLimit;
        public int FerverTime => _ferverTime;
        public float CustomerSpawnRate => _customerSpawnRate;
        public GimmickSettings TumbleWeed => _tumbleweed;
        public GimmickSettings Robber => _robber;
        public CatchSettings CatchSettings => _catchSettings;
        public ScoreSettings ScoreSettings => _scoreSettings;
        public ActorType RandomCustomerType => _spawnRateSettings.RandomActor;
    }
}
