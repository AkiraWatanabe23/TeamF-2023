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
        [Header("�A�C�e����ςވʒu�̃I�t�Z�b�g")]
        [SerializeField] Vector3 _offset;
        [Header("�ςވʒu�̃����_���Ȃ��炵��")]
        [SerializeField] float _randomShift = 0.1f;
        [Header("�ς߂�ő吔")]
        [SerializeField] int _maxStack = 6;
        [Header("�Œ�З�")]
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
        /// �ő吔�ɒB���Ă��Ȃ��ꍇ�́A�A�C�e����ς�ł���
        /// </summary>
        /// <returns>�ς�:true �ő吔�ɒB���Ă���:false</returns>
        public bool TryStack(ThrowedItem item)
        {
            // �ő吔�ς�ł���ꍇ�͒e��
            if (_tower.Count >= _maxStack) return false;

            // �����Đ�
            Cri.PlaySE("SE_ItemSet");

            // �����ʒu����̈ʒu���烉���_���ɂ��炷
            Vector3 shift = new Vector3(Random.Range(0, _randomShift), 0, Random.Range(0, _randomShift));

            // ��ԏ�ɐς�ŁA���ɐςލۂ̍������X�V����
            Vector3 stackPoint = transform.position + _offset + shift;
            stackPoint.y += _stackHeight;
            _stackHeight += item.Height;

            item.transform.position = stackPoint;
            item.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0); // ��]
            _tower.Enqueue(item);

            return true;
        }

        /// <summary>
        /// �ςݏグ���A�C�e���𓊂���
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            // 1�ȏ�ς�ł���ꍇ�͉����Đ�
            if (StackCount > 0) Cri.PlaySE("SE_Slide");

            // �Œ����ԋ�����ݒ�
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
