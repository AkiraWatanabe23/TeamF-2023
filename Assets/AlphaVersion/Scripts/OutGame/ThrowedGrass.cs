using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class ThrowedGrass : MonoBehaviour
    {
        [SerializeField] GameObject _particle;
        [Header("投げ当てた際に相手を吹っ飛ばす力")]
        [SerializeField] float _power = 5.0f;

        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                // オブジェクト自体が回転しているので左に力をかけると後ろにぶっ飛ぶ
                rb.AddForce(Vector3.left * _power, ForceMode.Impulse);
            }

            Cri.PlaySE("SE_BECHA");

            //Cri.PlaySE("SE_ItemCrash");
            Instantiate(_particle, transform.position, Quaternion.identity);
            StartCoroutine(C());
        }

        // 生成処理とかしてるので1フレーム後に消す
        IEnumerator C()
        {
            yield return null;
            Destroy(gameObject);
        }
    }
}
