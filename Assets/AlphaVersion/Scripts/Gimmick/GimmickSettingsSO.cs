using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ギミックに関する設定
    /// </summary>
    [CreateAssetMenu(fileName = "GimmickSettingsSO", menuName = "Settings/GimmickSettings")]
    [System.Serializable]
    public class GimmickSettingsSO : ScriptableObject
    {
        /// <summary>
        /// タンブルウィードギミック
        /// </summary>
        [System.Serializable]
        public class TumbleweedGimmick
        {
            [System.Serializable]
            public struct TimingData
            {
                [Min(0)]
                [SerializeField] float _elapsed;
                [Range(1, 20)]
                [SerializeField] int _count;

                public float Elapsed => _elapsed;
                public int Count => _count;
            }

            [Header("タイミング(秒)と生成する個数")]
            [SerializeField] TimingData[] _timing;

            public IReadOnlyList<TimingData> Timing => _timing;
            public int Max => _timing.Length;
        }

        /// <summary>
        /// 強盗ギミック
        /// </summary>
        [System.Serializable]
        public class RobberGimmick
        {
            [Min(0)]
            [Header("タイミング(秒)")]
            [SerializeField] float[] _timing;

            public IReadOnlyList<float> Timing => _timing;
            public int Max => _timing.Length;
        }

        [Header("タンブルウィードのギミック")]
        [SerializeField] TumbleweedGimmick _tumbleweed;
        [Header("強盗のギミック")]
        [SerializeField] RobberGimmick _robber;

        public TumbleweedGimmick Tumbleweed => _tumbleweed;
        public RobberGimmick Robber => _robber;
    }
}
