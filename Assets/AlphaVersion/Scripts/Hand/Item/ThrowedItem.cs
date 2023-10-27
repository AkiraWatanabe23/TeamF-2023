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
                Vector3 current = transform.position;
                current.y = 0;
                Vector3 start = _startingPoint;
                start.y = 0;
                return (current - start).sqrMagnitude;
            }
        }

        /// <summary>
        /// �w�肵������/�З͂œ�����
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = velocity;

            // �������ۂ̈ʒu��ێ�����
            _startingPoint = transform.position;
            // ���ɓ�����ꂽ�A�C�e���ł���t���O�𗧂Ă�
            IsThrowed = true;
        }
    }
}
