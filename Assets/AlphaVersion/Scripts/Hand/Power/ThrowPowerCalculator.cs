using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �J��������e�[�u���ւ̃��C�L���X�g��p���āA��΂������ƕ������v�Z����N���X�B
    /// ����������͎w�肵���ʒu����}�E�X�J�[�\���ւ̃x�N�g���A
    /// ������З͂̓}�E�X�J�[�\�����ړ����������Ō��܂�B
    /// </summary>
    public class ThrowPowerCalculator : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] TableRaycaster _tableRaycaster;
        [SerializeField] DistanceEvaluate _evaluate;

        Vector3 _startCursorViewPoint;
        Vector3 _endCursorViewPoint;
        bool _isHolding;

        public Vector3 StartingPoint { get; private set; }
        public Vector3 EndingPoint { get; private set; }

        /// <summary>
        /// �n�_����I�_�̕����ɈЗ͂̕������ړ������n�_
        /// </summary>
        public Vector3 PowerSizePoint
        {
            get
            {
                float sqr = (_startCursorViewPoint - _endCursorViewPoint).sqrMagnitude;
                Vector3 normalized = (EndingPoint - StartingPoint).normalized;
                return StartingPoint + normalized * sqr;
            }
        }

        void Update()
        {
            if (_isHolding) UpdateEndingPoint();
        }

        /// <summary>
        /// ��΂��������v�Z���邽�߂Ɏn�_���Z�b�g����
        /// �����ɁA�}�E�X�J�[�\���̈ړ��ʂňЗ͂��v�Z���邽�߂Ƀ}�E�X�J�[�\���̈ʒu��ێ�����B
        /// </summary>
        public void SetStartingPoint(Vector3 startingPoint)
        {
            _startCursorViewPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            StartingPoint = startingPoint;

            _isHolding = true;
        }

        /// <summary>
        /// �n�_����I�_�Ɍ��������x�x�N�g�����v�Z����B
        /// �����̓}�E�X�J�[�\���̈ړ��ʂŌ��肳���B
        /// </summary>
        /// <returns>�n�_����I�_�������A�}�E�X�J�[�\�����ړ����������𑬓x�ɂ����x�N�g��</returns>
        public Vector3 CalculateThrowVelocity()
        {
            if (_isHolding)
            {
                _isHolding = false;

                // �r���[�|�C���g���W�n��2�_��]���֐��ɒʂ���01�Ƀ��}�b�v����
                float remap = _evaluate.Evaluate(_endCursorViewPoint, _startCursorViewPoint);
                return (EndingPoint - StartingPoint) * _settings.Power * remap;

            }
            else
            {
                Debug.LogWarning("StartingPoint���Z�b�g���Ă��Ȃ�");
                return default;
            }
        }

        /// <summary>
        /// �}�E�X�J�[�\���̈ʒu�փ��C�L���X�g���s���A���C�����������ꏊ����Ƃ��A
        /// �I�_��y���W���n�_�Ɠ����l�ɂ��邱�ƂŁA�����ʏ�̓_�ɕ␳����B
        /// �����ɁA�}�E�X�J�[�\���̈ړ��ʂňЗ͂��v�Z���邽�߂Ƀ}�E�X�J�[�\���̈ʒu���X�V����B
        /// </summary>
        void UpdateEndingPoint()
        {
            _tableRaycaster.CameraToMousePointRay(out Vector3 rayHitPoint);
            EndingPoint = new Vector3(rayHitPoint.x, StartingPoint.y, rayHitPoint.z);

            // NOTE:�{���Ȃ�΃}�E�X�J�[�\���𗣂����ۂɂ̂ݍX�V����΂悢���A
            //      �З͂����o�����邽�߂ɓ����ɍX�V���Ă���B
            _endCursorViewPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        void OnDrawGizmos()
        {
            Draw2Points();
        }

        /// <summary>
        /// �n�_�ƏI�_�A2�_�����Ԑ���`�悷��
        /// </summary>
        void Draw2Points()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(StartingPoint, 0.05f);
            Gizmos.DrawWireSphere(EndingPoint, 0.05f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(StartingPoint, EndingPoint);
        }
    }
}
