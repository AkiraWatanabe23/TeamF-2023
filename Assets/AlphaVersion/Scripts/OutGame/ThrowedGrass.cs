using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class ThrowedGrass : MonoBehaviour
    {
        [SerializeField] GameObject _particle;

        void OnCollisionEnter(Collision collision)
        {
            //Cri.PlaySE("SE_ItemCrash_short"); 鳴るがシーン遷移で必ずエラー落ちするのでコメントアウト中
            Instantiate(_particle, transform.position, Quaternion.identity);
        }
    }
}
