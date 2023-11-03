using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum ItemType
    {
        Scotch,  // Glass01
        Bourbon, // Glass02
        Cognac,  // Glass03
        Potato,  // Potato01
    }

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

        HandSettingsSO _settings;
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

        /// <summary>
        /// �O�����琶�����ɏ���������AAwake�̑�p���\�b�h
        /// </summary>
        public void Init(HandSettingsSO settings)
        {
            _settings = settings;
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
                Crash();
            }
        }

        /// <summary>
        /// �j�􂳂���
        /// </summary>
        void Crash()
        {
            // TODO:���݂̓A�C�e���̍d���Ɋւ�炸�A�r��������鉹���Đ�����
            Cri.PlaySE("SE_ItemCrash_short");
            Vector3 particlePosition = transform.position + _settings.CrashParticleOffset;
            ParticleMessageSender.SendMessage(ParticleType.Crash, particlePosition);

            // TODO:�폜�������K�v
        }
    }
}

// �����:�A�C�e�����ɂ���̃Z�b�e�B���OSO���K�v�B�p�[�e�B�N���̐����̃I�t�Z�b�g�Ɏg������