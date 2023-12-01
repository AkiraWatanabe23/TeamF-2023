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

            [Header("�^�C�~���O(�b)�Ɛ��������")]
            [SerializeField] TimingData[] _timing;

            // �M�~�b�N���g��Ȃ��ꍇ�̓Q�[�����ԊO���w�肷��_�~�[�̃f�[�^�̔z�������ĕԂ�
            public IReadOnlyList<TimingData> Timing => _timing ??= new TimingData[1]
            {
                new TimingData(999.0f, 1),
            };
            public int Max => _timing.Length;
        }

        /// <summary>
        /// �����M�~�b�N
        /// </summary>
        [System.Serializable]
        public class RobberGimmick
        {
            [Min(0)]
            [Header("�^�C�~���O(�b)")]
            [SerializeField] float[] _timing;

            // �M�~�b�N���g��Ȃ��ꍇ��null�ɂȂ�̂ŃQ�[�����ԊO���w�肷��z�������ĕԂ�
            public IReadOnlyList<float> Timing => _timing ??= new float[1] { 999 };
            public int Max => _timing.Length;
        }

        /// <summary>
        /// UFO�M�~�b�N
        /// </summary>
        [System.Serializable]
        public class UFOGimmick
        {
            [Min(0)]
            [Header("�^�C�~���O(�b)")]
            [SerializeField] float[] _timing;

            // �M�~�b�N���g��Ȃ��ꍇ��null�ɂȂ�̂ŃQ�[�����ԊO���w�肷��z�������ĕԂ�
            public IReadOnlyList<float> Timing => _timing ??= new float[1] { 999 };
            public int Max => _timing.Length;
        }

        [Header("�^���u���E�B�[�h�̃M�~�b�N")]
        [SerializeField] TumbleweedGimmick _tumbleweed;
        [Header("�����̃M�~�b�N")]
        [SerializeField] RobberGimmick _robber;
        [Header("UFO�̃M�~�b�N")]
        [SerializeField] UFOGimmick _ufo;

        public TumbleweedGimmick Tumbleweed => _tumbleweed;
        public RobberGimmick Robber => _robber;
        public UFOGimmick UFO => _ufo;
    }
}
