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
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] SpawnRangeChecker _checker;
        [Header("デバッグ用: Zキーで客/Cキーで強盗を生成")]
        [SerializeField] bool _isDebug;

        void Update()
        {
            if (_isDebug)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _initializer.Initialize(BehaviorType.Customer, ActorType.Male);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
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
                // 生成するキャラクターは重み付きで抽選される
                _initializer.Initialize(BehaviorType.Customer, _settings.RandomCustomerType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 強盗の生成を行う
        /// </summary>
        public void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
        }
    }
}