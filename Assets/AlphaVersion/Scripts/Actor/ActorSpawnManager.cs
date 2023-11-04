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
            // テスト用:キー入力でキャラクターを生成する
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