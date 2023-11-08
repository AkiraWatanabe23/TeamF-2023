using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 一定間隔でキャラクターの生成を行うを管理するクラス
    /// </summary>
    public class ActorSpawnManager : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] ActorSpawnTimer _timer;
        [SerializeField] SpawnRangeChecker _checker;
        [SerializeField] GimmickProvider _gimmick; // TODO:本来ならインターフェースで参照
        [Header("デバッグ用: Zキーで客/Cキーで強盗を生成")]
        [SerializeField] bool _isDebug;

        void OnEnable()
        {
            // インゲーム開始でタイマーがスタートするので、現状ゲーム開始フラグをチェックする必要が無い
            _timer.OnSpawnTiming += TrySpawnCustomer;
            _gimmick.OnRobberSpawned += SpawnRobber;
        }

        void OnDisable()
        {
            _timer.OnSpawnTiming -= TrySpawnCustomer;
            _gimmick.OnRobberSpawned -= SpawnRobber;
        }

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
        void TrySpawnCustomer()
        {
            // TODO:キャラクターの生成は客、強盗、ギミックがあるが、現状客だけの場合で作っている。
            if (!_isDebug && _checker.Check())
            {
                _initializer.Initialize(BehaviorType.Customer, ActorType.Male);
            }
        }

        /// <summary>
        /// 強盗の生成を行う
        /// </summary>
        void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
        }
    }
}