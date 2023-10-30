using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���S�Ă����ʂ��Ď��R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowedItem : MonoBehaviour
    {
        [Header("���点��ۂɕK�v�Ȓl�̐ݒ�")]
        [Range(0, 1.0f)]
        [SerializeField] float _hardness;
        [Header("�ςލۂɕK�v�Ȓl�̐ݒ�")]
        [SerializeField] float _height = 0.25f;

        Vector3 _startingPoint;
        public bool IsThrowed { get; private set; }

        public float Height => _height;

        /// <summary>
        /// �����Ɉړ�����������2���Ԃ�
        /// </summary>
        public float MovingSqrDistance
        {
            get
            {
                Vector3 current = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 start = new Vector3(_startingPoint.x, 0, _startingPoint.z);
                return (current - start).sqrMagnitude;
            }
        }

        void Awake()
        {
            FreezeXZ();
        }

        /// <summary>
        /// ����Ȃ��悤��X��Z�����ւ̈ړ��𐧌�����
        /// </summary>
        void FreezeXZ()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                             RigidbodyConstraints.FreezePositionZ;
        }

        /// <summary>
        /// �w�肵������/�З͂œ�����
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = velocity;

            // �������ۂ̈ʒu��ێ�����
            _startingPoint = transform.position;
            // ���ɓ�����ꂽ�A�C�e���ł���t���O�𗧂Ă�
            IsThrowed = true;
        }

        void OnCollisionEnter(Collision collision)
        {
            // ������
            if (collision.gameObject.TryGetComponent(out FloorMarker _))
            {
                Crush();
            }
        }

        /// <summary>
        /// �j�􂳂���
        /// </summary>
        void Crush()
        {
            // TODO:���݂̓A�C�e���̍d���Ɋւ�炸�A�r��������鉹���Đ�����
            Cri.PlaySE("SE_ItemCrash_short");

            // TODO:�폜�������K�v
        }
    }
}