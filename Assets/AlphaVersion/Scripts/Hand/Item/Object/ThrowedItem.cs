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
        Beef,    // RoastBeef
        MiniActor, // �����ɂ��邱�ƂŔ��肷��̂ź�
    }

    /// <summary>
    /// ������A�C�e���S�Ă����ʂ��Ď��R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowedItem : MonoBehaviour, ICatchable
    {
        ThrowedItemPool _pool; // �v�[��
        ItemSettingsSO _settings;
        Rigidbody _rigidbody;
        Vector3 _startingPoint;
        RigidbodyConstraints _defaultConstraints;
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
        /// �������ăv�[���ɒǉ������ۂ�1�x�����v�[��������Ăяo����郁�\�b�h
        /// </summary>
        public void OnCreate(ThrowedItemPool pool)
        {
            _pool = pool;
            _defaultConstraints = GetComponent<Rigidbody>().constraints;
        }

        /// <summary>
        /// �O������v�[��������o�����ۂɏ���������AAwake�̑�p���\�b�h
        /// </summary>
        public void Init(ItemSettingsSO settings)
        {
            IsThrowed = false;
            
            _settings = settings;

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

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
            _rigidbody.constraints = _defaultConstraints;
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

            // ���ɓ�����ꂽ���
            if (IsThrowed)
            {
                // �A�C�e���ƂԂ�����
                if (collision.gameObject.TryGetComponent(out ThrowedItem _))
                {
                    // ���炷
                    Cri.PlaySE(_settings.HitSEName);
                }
                // �L�����N�^�[�ɂԂ������B�q�ɃR���C�_�[������A�e�ɃX�N���v�g������
                if (collision.transform.parent != null && 
                    collision.transform.parent.TryGetComponent(out Actor _))
                {
                    Crash();
                }
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

            _pool.Return(this);
        }

        /// <summary>
        /// �����Ƃ��ăL���b�`���ꂽ�ۂɌĂ΂��
        /// </summary>
        public void Catch()
        {
            _pool.Return(this);
        }
    }
}
