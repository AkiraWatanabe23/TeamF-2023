using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���C���J��������e�[�u���֌��������C�L���X�g���s���N���X�B
    /// </summary>
    public class TableRaycaster : MonoBehaviour
    {
        [Header("�e�[�u���֔�΂����C�̐ݒ�")]
        [SerializeField] LayerMask _tableLayer;
        [SerializeField] float _distance = 100;

        /// <summary>
        /// ���C���J��������}�E�X�J�[�\���̈ʒu�փ��C�L���X�g���Ĕ�����s���B
        /// rayHitPoint �ɂ̓e�[�u���ȊO�Ƀq�b�g�����ꍇ�ł����̍��W����������B
        /// ���Ȃ������ꍇ��default����������B
        /// </summary>
        /// <returns>���C���e�[�u���Ƀq�b�g����:true ���Ȃ�����:false</returns>
        public bool CameraToMousePointRay(out Vector3 rayHitPoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // �M�Y���֕`��
            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.blue);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _tableLayer))
            {
                rayHitPoint = hitInfo.point;
                // �e�[�u���̃R���|�[�l���g�������Ă��邩�Ŕ���
                return hitInfo.collider.TryGetComponent(out TableMarker _);
            }
            else
            {
                rayHitPoint = default;
                return false;
            }
        }
    }
}