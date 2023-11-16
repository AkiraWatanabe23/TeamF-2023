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
        [Header("���������������ɉ������З͂̑����")]
        [SerializeField] AnimationCurve _evaluate;
        [Header("������З͂̔{��")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _power = 6.0f;
        [Header("�Œ�З�")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _minPower = 1.0f;
        [Header("�ς߂�ő吔")]
        [SerializeField] int _maxStack = 6;
        [Header("�ςވʒu�̃����_���Ȃ��炵��")]
        [Range(0, 0.1f)]
        [SerializeField] float _randomShift = 0.03f;
        [Header("�ςލۂɋ����I�ɃA�C�e���������͈�")]
        [SerializeField] float _throwedAreaSqrRadius = 0.31f;

        public AnimationCurve Evaluate => _evaluate;
        public float Power => _power;
        public float MinPower => _minPower;
        public int MaxStack => _maxStack;
        public float RandomShift => _randomShift;
        public float ThrowedAreaSqrRadius => _throwedAreaSqrRadius;
    }
}
