using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// �w�肵��2�_�Ԃ��ړ�����@�\�̃N���X
    /// ���t���[���i�s�����Ƀ��C�L���X�g���A��Q�������邩���`�F�b�N����@�\������
    /// </summary>
    public class MoveBetween : MonoBehaviour
    {
        [SerializeField] Collider _collider;
        [Header("�i�s�����ւ̃��C�L���X�g�̐ݒ�")]
        [SerializeField] LayerMask _actorLayer;
        [SerializeField] float _rayDistance = 10.0f;
        [SerializeField] float _eyeHeight = 0.75f;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// �����̑��x�� from ���� to �ֈړ�����
        /// �i�s�����ɏ�Q��������ꍇ�́A���̏�Ŏ~�߂邱�Ƃ��o����B
        /// </summary>
        public async UniTask MoveAsync(float speed, Vector3 from, Vector3 to, CancellationToken token, bool useCheck = true)
        {
            float lerpProgress = 0;
            float sqrDistance = (to - from).sqrMagnitude;
            while (lerpProgress < 1)
            {
                _transform.position = Vector3.Lerp(from, to, lerpProgress);

                if (IsClearForward(to))
                {
                    lerpProgress += speed * Time.deltaTime / sqrDistance;
                }

                await UniTask.Yield(cancellationToken: token);
            }
        }

        /// <summary>
        /// ���C�L���X�g�Ői�s�����ɃL�����N�^�[�����݂��邩�𔻒肷��B
        /// </summary>
        bool IsClearForward(Vector3 to)
        {
            if (Physics.Raycast(RayToNextVertex(to), out RaycastHit hit, _actorLayer))
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
        Ray RayToNextVertex(Vector3 to)
        {
            // �L�����N�^�[�̊�̈ʒu�������̊�ɁA���������ւ�Ray
            Vector3 origin = _transform.position + Vector3.up * _eyeHeight;
            to = new Vector3(to.x, origin.y, to.z);
            Vector3 direction = (to - origin).normalized;

            return new Ray(origin, direction * _rayDistance);
        }
    }
}