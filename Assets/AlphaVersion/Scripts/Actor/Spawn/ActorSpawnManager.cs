using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// キャラクターの生成を行うを管理するクラス
    /// </summary>
    public class ActorSpawnManager : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] SpawnRangeChecker _checker;
        [Header("デバッグ用: Zキーで客/Cキーで強盗を生成")]
        [SerializeField] bool _isDebug;

        /// <summary>
        /// 男か女かを同じ確率のランダム
        /// </summary>
        ActorType RandomCustomer => Random.value <= 0.5f ? ActorType.Male : ActorType.Female;

        void Update()
        {
            if (_isDebug)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _initializer.Initialize(BehaviorType.Customer, RandomCustomer);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    _initializer.Initialize(BehaviorType.Robber, ActorType.Muscle);
                }
            }
        }

        /// <summary>
        /// 生成範囲に既にキャラクターがいる場合は生成を行わない
        /// この場合、次の生成タイミングまで待つ。
        /// </summary>
        public bool TrySpawnRandomCustomer()
        {
            if (!_isDebug && _checker.Check())
            {
                _initializer.Initialize(BehaviorType.Customer, RandomCustomer);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 強盗の生成を行う
        /// </summary>
        public void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Muscle);
        }
    }
}