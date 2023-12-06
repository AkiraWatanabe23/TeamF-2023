using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ��̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "HandSettingsSO", menuName = "Settings/HandSettings")]
    [System.Serializable]
    public class HandSettingsSO : ScriptableObject
    {
        /// <summary>
        /// �t�B�[�o�[�^�C�����Ƀ~�j�L�����N�^�[�𓊂���m��
        /// �v�����i�[�ɘM�点��ꍇ�͐K������
        /// </summary>
        public readonly float FerverMiniActorPercent = float.MaxValue;

        [Header("<color=#00FF76>���������������ɉ������З͂̑����</color>")]
        [SerializeField] AnimationCurve _evaluate;
        [Header("<color=#00FF76>������З͂̔{��</color>")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _power = 6.0f;
        [Header("<color=#00FF76>�Œ�З�</color>")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _minPower = 1.0f;
        [Header("<color=#00FF76>�ς߂�ő吔</color>")]
        [Range(1, 6)]
        [SerializeField] int _maxStack = 6;
        [Header("<color=#00FF76>�ςވʒu�̃����_���Ȃ��炵��</color>")]
        [Range(0, 0.1f)]
        [SerializeField] float _randomShift = 0.03f;
        [Header("<color=#00FF76>�ςލۂɋ����I�ɃA�C�e���������͈�</color>")]
        [SerializeField] float _throwedAreaSqrRadius = 0.31f;
        [Header("<color=#00FF76>�����̍U�����󂯂��ۂ̃y�i���e�B(�b)</color>")]
        [SerializeField] float _damagedPenalty = 2.0f;

        public AnimationCurve Evaluate => _evaluate;
        public float Power => _power;
        public float MinPower => _minPower;
        public int MaxStack => _maxStack;
        public float RandomShift => _randomShift;
        public float ThrowedAreaSqrRadius => _throwedAreaSqrRadius;
        public float DamagedPenalty => _damagedPenalty;
    }
}
