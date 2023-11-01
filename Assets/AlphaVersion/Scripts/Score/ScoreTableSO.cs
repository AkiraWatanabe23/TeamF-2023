using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �X�R�A�̌v�Z�Ɋւ���l�̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreTableSO", menuName = "ScoreData")]
    public class ScoreTableSO : ScriptableObject
    {
        /// <summary>
        /// �e�A�N�V�����̑����̊�ƂȂ�X�R�A�̒l
        /// </summary>
        const int BaseActionScore = 100;

        /// <summary>
        /// �q�E�����̊��N���X
        /// </summary>
        [System.Serializable]
        public class Actor
        {
            [Header("���������ۂ̉��Z��")]
            [SerializeField] int SuccessBonus = BaseActionScore;
            [Header("���s�����ۂ̌�����")]
            [SerializeField] int FailurePenalty = BaseActionScore;
        }

        [Header("����")]
        [SerializeField] Actor _robber;
        [Header("�q(�j)")]
        [SerializeField] Actor _male;
        [Header("�q(��)")]
        [SerializeField] Actor _female;
        [Header("�q(���L���L)")]
        [SerializeField] Actor _muscle;
        [Header("�ʏ펞�̔{��")]
        [SerializeField] float _defaultScoreRate = 1;
        [Header("�t�B�[�o�[�^�C���̔{��")]
        [SerializeField] float _feverScoreRate = 2;

        public Actor Robber => _robber;
        public Actor Male => _male;
        public Actor Female => _female;
        public Actor Muscle => _muscle;
        public float DefaultScoreRate => _defaultScoreRate;
        public float FeverScoreRate => _feverScoreRate;
    }
}
