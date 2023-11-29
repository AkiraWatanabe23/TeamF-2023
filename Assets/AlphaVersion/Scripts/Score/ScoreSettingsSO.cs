using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Alpha
{
    /// <summary>
    /// �X�R�A�Ɋւ���ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreSettingsSO", menuName = "Settings/ScoreSettings")]
    [System.Serializable]
    public class ScoreSettingsSO : ScriptableObject
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
        [Header("�ʏ펞�̔{��")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("�t�B�[�o�[�^�C���̔{��")]
        [SerializeField] float _feverScoreRate = 2;

        public ActorScore Robber => _robber;
        public ActorScore Male => _male;
        public ActorScore Female => _female;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
