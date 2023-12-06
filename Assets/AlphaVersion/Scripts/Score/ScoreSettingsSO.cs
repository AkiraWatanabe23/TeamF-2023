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
            [Header("<color=#00FF76>���������ۂ̉��Z��</color>")]
            public int SuccessBonus = BaseActionScore;
            [Header("<color=#00FF76>���s�����ۂ̌�����</color>")]
            public int FailurePenalty = BaseActionScore;
        }

        [Header("<color=#00FF76>����</color>")]
        [SerializeField] ActorScore _robber;
        [Header("<color=#00FF76>�q(�j)</color>")]
        [SerializeField] ActorScore _male;
        [Header("<color=#00FF76>�q(��)</color>")]
        [SerializeField] ActorScore _female;
        [Header("<color=#00FF76>�ʏ펞�̔{��</color>")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("<color=#00FF76>�t�B�[�o�[�^�C���̔{��</color>")]
        [SerializeField] float _feverScoreRate = 2;

        public ActorScore Robber => _robber;
        public ActorScore Male => _male;
        public ActorScore Female => _female;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
