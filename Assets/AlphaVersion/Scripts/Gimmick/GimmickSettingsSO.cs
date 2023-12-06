using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �M�~�b�N�Ɋւ���ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "GimmickSettingsSO", menuName = "Settings/GimmickSettings")]
    [System.Serializable]
    public class GimmickSettingsSO : ScriptableObject
    {
        /// <summary>
        /// �^���u���E�B�[�h�M�~�b�N
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

                public TimingData(float elapsed, int count)
                {
                    _elapsed = elapsed;
                    _count = count;
                }

                public float Elapsed => _elapsed;
                public int Count => _count;
            }

            [Header("<color=#00FF76>�^�C�~���O(�b)�Ɛ��������</color>")]
            [SerializeField] TimingData[] _timing;

            public IReadOnlyList<TimingData> Timing
            {
                get
                {
                    if (_timing == null || _timing.Length == 0)
                    {
                        _timing = new TimingData[1] { new TimingData(float.MaxValue, 1) };
                    }
                    return _timing;
                }
            }
            public int Max => _timing.Length;
        }

        /// <summary>
        /// �����M�~�b�N
        /// </summary>
        [System.Serializable]
        public class RobberGimmick
        {
            [Min(0)]
            [Header("<color=#00FF76>�^�C�~���O(�b)</color>")]
            [SerializeField] float[] _timing;

            public IReadOnlyList<float> Timing
            {
                get
                {
                    if (_timing == null || _timing.Length == 0)
                    {
                        _timing = new float[1] { float.MaxValue };
                    }
                    return _timing;
                }
            }
            public int Max => _timing.Length;
        }

        /// <summary>
        /// UFO�M�~�b�N
        /// </summary>
        [System.Serializable]
        public class UFOGimmick
        {
            [Min(0)]
            [Header("<color=#00FF76>�^�C�~���O(�b)</color>")]
            [SerializeField] float[] _timing;

            public IReadOnlyList<float> Timing
            {
                get
                {
                    if (_timing == null || _timing.Length == 0)
                    {
                        _timing = new float[1] { float.MaxValue };
                    }
                    return _timing;
                }
            }
            public int Max => _timing.Length;
        }

        [Header("<color=#00FF76>�^���u���E�B�[�h�̃M�~�b�N</color>")]
        [SerializeField] TumbleweedGimmick _tumbleweed;
        [Header("<color=#00FF76>�����̃M�~�b�N</color>")]
        [SerializeField] RobberGimmick _robber;
        [Header("<color=#00FF76>UFO�̃M�~�b�N</color>")]
        [SerializeField] UFOGimmick _ufo;

        public TumbleweedGimmick Tumbleweed => _tumbleweed;
        public RobberGimmick Robber => _robber;
        public UFOGimmick UFO => _ufo;
    }
}
