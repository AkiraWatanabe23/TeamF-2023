using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �ړ������ɏ�Q�������邩�����C�L���X�g��p���Ĕ��肷��@�\�̃N���X
    /// </summary>
    public class MoveRaycaster : MonoBehaviour
    {
        [Header("�O���𒲂ׂ郌�C�̐ݒ�")]
        [SerializeField] float _rayDistance = 1.0f;
        [SerializeField] float _eyeHeight = 0.75f;
        [SerializeField] LayerMask _actorLayer;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// ���C�L���X�g�Ői�s�����ɃL�����N�^�[�����݂��邩�𔻒肷��B
        /// </summary>
        public bool IsClearForward(Vector3 to)
        {
            if (Physics.Raycast(GetRay(to), out RaycastHit hit, _rayDistance, _actorLayer))
            {
                // �L�����N�^�[�̊��N���X�̃R���|�[�l���g�������Ă���Ŕ���
                if (hit.collider.TryGetComponent(out Actor actor) &&
                    actor.GetInstanceID() != GetInstanceID())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ���ݒn���� to �Ɍ����Ẵ��C��Ԃ��B
        /// </summary>
        Ray GetRay(Vector3 to)
        {
            // �L�����N�^�[�̊�̈ʒu�������̊�ɁA���������ւ�Ray
            Vector3 origin = _transform.position + Vector3.up * _eyeHeight;
            to = new Vector3(to.x, origin.y, to.z);
            Vector3 direction = (to - origin).normalized;

            // �M�Y���ɕ`��
            Debug.DrawRay(origin, direction * _rayDistance, Color.red);

            return new Ray(origin, direction);
        }
    }
}
