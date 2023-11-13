using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �M�~�b�N�Ɋւ���ݒ�
    /// </summary>
    [System.Serializable]
    public class GimmickSettings
    {
        [Header("�M�~�b�N�����̃^�C�~���O(�b)")]
        [SerializeField] float[] _timing;
        [Header("�^�C�~���O�̃����_����")]
        [Range(0, 1.0f)]
        [SerializeField] float _random = 1.0f;

        public IReadOnlyList<float> Timing => _timing ??= new float[0];
        public float FixedDelta => Random.Range(1.0f - _random, 1.0f);
    }

    /// <summary>
    /// �L���b�`����Ɋւ���ݒ�
    /// </summary>
    [System.Serializable]
    public class CatchSettings
    {
        [Header("�傫���̐ݒ�")]
        [Range(0.1f, 0.8f)]
        [SerializeField] float _normalSize = 0.25f;
        [Range(0.1f, 0.8f)]
        [SerializeField] float _ferverSize = 0.8f;
        [Header("���i���L���b�`�ł��鑬�x")]
        [SerializeField] float _catchableSpeed = 1.0f;

        public float NormalSize => _normalSize;
        public float FerverSize => _ferverSize;
        public float CatchableSpeed => _catchableSpeed;
    }

    /// <summary>
    /// �X�R�A�Ɋւ���ݒ�
    /// </summary>
    [System.Serializable]
    public class ScoreSettings
    {
        // �e�A�N�V�����ő�������X�R�A�̊�l
        const int BaseActionScore = 100;

        // �q�E�����̊��N���X
        [System.Serializable]
        public class ActorScore
        {
            [Header("���������ۂ̉��Z��")]
            public int SuccessBonus = BaseActionScore;
            [Header("���s�����ۂ̌�����")]
            public int FailurePenalty = BaseActionScore;
        }

        [Header("����")]
        [SerializeField] ActorScore _robber;
        [Header("�q(�j)")]
        [SerializeField] ActorScore _male;
        [Header("�q(��)")]
        [SerializeField] ActorScore _female;
        [Header("�q(���L���L)")]
        [SerializeField] ActorScore _muscle;
        [Header("�ʏ펞�̔{��")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("�t�B�[�o�[�^�C���̔{��")]
        [SerializeField] float _feverScoreRate = 2;

        public ActorScore Robber => _robber;
        public ActorScore Male => _male;
        public ActorScore Female => _female;
        public ActorScore Muscle => _muscle;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }

    /// <summary>
    /// �q�̐����m���Ɋւ���ݒ�
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
        /// �d�ݕt���m�����I�Ő�������L�����N�^�[��I��
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
    /// �C���Q�[���̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("��������")]
        [SerializeField] int _timeLimit = 60;
        [Header("�t�B�[�o�[�^�C���J�n")]
        [SerializeField] int _ferverTime = 20;
        [Header("�q�̐����Ԋu(�b)")]
        [SerializeField] float _customerSpawnRate = 3.0f;
        [Header("�^���u���E�B�[�h�̃M�~�b�N�̐ݒ�")]
        [SerializeField] GimmickSettings _tumbleweed;
        [Header("�����̃M�~�b�N�̐ݒ�")]
        [SerializeField] GimmickSettings _robber;
        [Header("�q���L���b�`���锻��̐ݒ�")]
        [SerializeField] CatchSettings _catchSettings;
        [Header("�q���̐����m���ݒ�")]
        [SerializeField] SpawnRateSettings _spawnRateSettings;
        [Header("�X�R�A�̐ݒ�")]
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
