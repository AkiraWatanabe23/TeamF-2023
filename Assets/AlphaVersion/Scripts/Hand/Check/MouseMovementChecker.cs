using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �}�E�X���ړ��������ǂ����𔻒肷��N���X
    /// 2�_�Ԃ̋�����臒l�ȉ����ǂ����Ŕ�����s��
    /// </summary>
    public class MouseMovementChecker : MonoBehaviour
    {
        [Header("�ړ����Ă��Ȃ��Ɣ��肳��鋗��")]
        [SerializeField] float Threshold = 0.001f;

        Vector3 _startingPoint;
        bool _isHolding;

        /// <summary>
        /// ���݂̃}�E�X�J�[�\���̈ʒu���n�_�Ƃ���
        /// </summary>
        public void SetStartingPoint()
        {
            _startingPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            _isHolding = true;
        }

        /// <summary>
        /// �n�_���猻�݂̃}�E�X�J�[�\���̍��W�܂ł̋�����臒l�ȉ��Ȃ瓮���Ă��Ȃ��Ɣ���
        /// StartingPoint ���ݒ肳��Ă��Ȃ��ꍇ�� true ��Ԃ�
        /// </summary>
        public bool IsMoved()
        {
            if (_isHolding)
            {
                Vector3 endingPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                return (endingPoint - _startingPoint).sqrMagnitude > Threshold;
            }
            else
            {
                Debug.LogWarning("StartingPoint���Z�b�g���Ă��Ȃ�");
                return true;
            }
        }
    }
}
