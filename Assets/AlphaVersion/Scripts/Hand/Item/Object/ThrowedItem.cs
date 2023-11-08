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
    public class ThrowedItem : MonoBehaviour, ICatchable
    {
        ItemSettingsSO _settings;
        Rigidbody _rigidbody;
        Vector3 _startingPoint;
        public bool IsThrowed { get; private set; }

        public float Height => _settings.Height;

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

        public ItemType Type => _settings.Type;
        public float SqrSpeed => _rigidbody.velocity.sqrMagnitude;

        /// <summary>
        /// �O�����琶�����ɏ���������AAwake�̑�p���\�b�h
        /// </summary>
        public void Init(ItemSettingsSO settings)
        {
            _settings = settings;
            _rigidbody = GetComponent<Rigidbody>();
            FreezeXZ();
        }

        /// <summary>
        /// ����Ȃ��悤��X��Z�����ւ̈ړ��𐧌�����
        /// </summary>
        void FreezeXZ()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
                                     RigidbodyConstraints.FreezePositionZ;
        }

        /// <summary>
        /// �w�肵������/�З͂œ�����
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.velocity = velocity;

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
            // ���ɓ�����ꂽ��ԂŃA�C�e���ƂԂ�����
            if (IsThrowed && collision.gameObject.TryGetComponent(out ThrowedItem item))
            {
                // ���炷
                Cri.PlaySE(_settings.HitSEName);
            }
        }

        /// <summary>
        /// �j�􂳂���
        /// </summary>
        void Crash()
        {          
            // ���ƃp�[�e�B�N��
            Cri.PlaySE(_settings.CrashSEName);
            Vector3 particlePosition = transform.position + _settings.CrashParticleOffset;
            ParticleMessageSender.SendMessage(_settings.CrashParticle, particlePosition);

            // TODO:�폜�������K�v
        }

        /// <summary>
        /// �����Ƃ��ăL���b�`���ꂽ�ۂɌĂ΂��
        /// </summary>
        public void OnCatched()
        {
            Debug.Log("�L���b�`���ꂽ");
        }
    }
}

// �L���b�`�����ۂɏ����Ȃ�
// �_���u���E�B�[�h�~��M�~�b�N