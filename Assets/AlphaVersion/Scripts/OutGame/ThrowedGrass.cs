using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class ThrowedGrass : MonoBehaviour
    {
        [SerializeField] GameObject _particle;
        [Header("�������Ă��ۂɑ���𐁂���΂���")]
        [SerializeField] float _power = 5.0f;

        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                // �I�u�W�F�N�g���̂���]���Ă���̂ō��ɗ͂�������ƌ��ɂԂ����
                rb.AddForce(Vector3.left * _power, ForceMode.Impulse);
            }

            Cri.PlaySE("SE_BECHA");

            //Cri.PlaySE("SE_ItemCrash");
            Instantiate(_particle, transform.position, Quaternion.identity);
            StartCoroutine(C());
        }

        // ���������Ƃ����Ă�̂�1�t���[����ɏ���
        IEnumerator C()
        {
            yield return null;
            Destroy(gameObject);
        }
    }
}
