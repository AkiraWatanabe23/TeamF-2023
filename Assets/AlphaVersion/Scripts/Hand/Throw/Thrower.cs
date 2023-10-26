using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���� �ς�/������ ���s���N���X
    /// </summary>
    public class Thrower : MonoBehaviour
    {
        [SerializeField] ThrowedItem _prefab;
        [Header("�A�C�e����ςލۈʒu�̃I�t�Z�b�g")]
        [SerializeField] Vector3 _offset;
        [Header("�C��:�Œ�З�")]
        [SerializeField] float _minPower = 0;

        Queue<ThrowedItem> _tower = new();
        float _stackHeight;

        /// <summary>
        /// �A�C�e����ςވʒu
        /// ���̃I�u�W�F�N�g�̍��W�ɃI�t�Z�b�g�𑫂����ʒu
        /// </summary>
        public Vector3 StackPoint => transform.position + _offset;      
        /// <summary>
        /// ���ݐς�ł��鐔
        /// </summary>
        public int StackCount => _tower.Count;

        /// <summary>
        /// �A�C�e����ς�ł���
        /// </summary>
        public void Stack(ThrowedItem item)
        {
            // ��ԏ�ɐς�ŁA���ɐςލۂ̍������X�V����
            Vector3 spawnPoint = transform.position + _offset;
            spawnPoint.y += _stackHeight;
            _stackHeight += item.Height;

            item.transform.position = spawnPoint;
            _tower.Enqueue(item);
        }

        /// <summary>
        /// �ςݏグ���A�C�e���𓊂���
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            // �C��:�Œ�З͂𑫂����ƂŎw�肵�������͕K����Ԃ悤�ɂȂ�
            Vector3 minVelocity = velocity.normalized * _minPower;

            foreach (ThrowedItem item in _tower)
            {
                item.Throw(velocity + minVelocity);
            }

            // 1����ςނ��߂Ɋe�l�����Z�b�g
            _stackHeight = 0;
            _tower.Clear();
        }

        void OnDrawGizmos()
        {
            DrawStackPoint();
        }

        /// <summary>
        /// �A�C�e����ςވʒu���M�Y���ɕ`�悷��
        /// </summary>
        void DrawStackPoint()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StackPoint, 0.05f);
        }
    }
}
