using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ����������ƈЗ͂����o������@�\�̃N���X
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class ThrowPowerVisualizer : MonoBehaviour
    {
        [Header("���̒����̔{��")]
        [SerializeField] float _lengthPower = 1;

        LineRenderer _line;

        void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// 2�_�Ԃ̊ԂɃ��b�V����`�悷��
        /// </summary>
        public void Draw(Vector3 startPoint, Vector3 goalPoint)
        {
            _line.enabled = true;

            _line.SetPosition(0, startPoint);
            _line.SetPosition(1, goalPoint);
        }

        /// <summary>
        /// �`�悵�Ă��郁�b�V�����폜����
        /// </summary>
        public void Delete()
        {
            _line.SetPosition(0, default);

            _line.enabled = false;
        }
    }
}
