using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �E�N���b�N�Ŏˏo�����G�Ђ̃N���X
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DustCloth : MonoBehaviour
    {
        /// <summary>
        /// �w�肵�������֗͂������Ďˏo����
        /// </summary>
        public void Shoot(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(velocity, ForceMode.Impulse);

            // ���ƃp�[�e�B�N��
            Cri.PlaySE("SE_Cleaning_1");
            ParticleMessageSender.SendMessage(ParticleType.Clean, transform.position, transform);
        }

        void OnCollisionEnter(Collision collision)
        {
            // ������
            if (collision.gameObject.TryGetComponent(out FloorMarker _))
            {
                Delete();
            }
        }

        /// <summary>
        /// ���ɂ����ꍇ�ɍ폜����
        /// </summary>
        void Delete()
        {
            Destroy(gameObject);
        }
    }
}
