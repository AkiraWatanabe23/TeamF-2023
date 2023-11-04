using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class ActorSpawnManager : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;

        void Start()
        {

        }

        void Update()
        {
            // �e�X�g�p:�L�[���͂ŃL�����N�^�[�𐶐�����
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _initializer.Initialize(BehaviorType.Customer);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _initializer.Initialize(BehaviorType.Robber);
            }
        }
    }
}