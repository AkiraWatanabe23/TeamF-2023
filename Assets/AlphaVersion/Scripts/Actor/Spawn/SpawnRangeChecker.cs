using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �L�����N�^�[�̐������\�����`�F�b�N����@�\�̃N���X
    /// �`�F�b�N�������ӏ��̌��ɉ����Đݒu����
    /// </summary>
    public class SpawnRangeChecker : MonoBehaviour
    {
        [SerializeField] Transform _spawnPoint;
        [SerializeField] LayerMask _actorLayer;
        [SerializeField] float _radius;

        /// <summary>
        /// �͈͓��ɃL�����N�^�[�����邩�ǂ����𔻒肷��
        /// </summary>
        public bool Check()
        {
            // TODO:NonAlloc�ɕύX����
            Collider[] result = Physics.OverlapSphere(_spawnPoint.position, _radius, _actorLayer);
            foreach (Collider collider in result)
            {
                if (collider.transform.parent.TryGetComponent(out Actor _))
                {
                    return false;
                }
            }

            return true;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_spawnPoint.position, _radius);
        }
    }
}