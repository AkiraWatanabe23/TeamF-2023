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
            //Cri.PlaySE("SE_ItemCrash_short"); �邪�V�[���J�ڂŕK���G���[��������̂ŃR�����g�A�E�g��
            Instantiate(_particle, transform.position, Quaternion.identity);
        }
    }
}
