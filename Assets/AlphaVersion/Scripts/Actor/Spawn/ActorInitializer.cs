using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 生成したキャラクターの初期化を行うクラス
    /// ManagerとSpawnerの間で、処理を挟んで橋渡しを行う
    /// </summary>
    public class ActorInitializer : FerverHandler
    {
        [SerializeField] ActorSpawner _spawner;
        [Header("初期化に必要なもの")]
        [SerializeField] PathCreator _pathCreator;
        [SerializeField] TableManager _tableManager;

        /// <summary>
        /// 生成したキャラクターを初期化して返す
        /// </summary>
        /// <returns>生成した初期化済みのキャラクター</returns>
        public Actor Initialize(BehaviorType behavior, ActorType actor)
        {
            Actor instance = _spawner.Spawn(behavior, actor);
            Waypoint lead = _pathCreator.GetPath(ToPathType(behavior));

            // 経路と席、現在フィーバータイムかどうかを渡す
            instance.Init(lead, _tableManager, Tension);

            return instance;
        }

        /// <summary>
        /// 引数の振る舞いに対応した経路の種類を返す
        /// </summary>
        /// <returns></returns>
        PathType ToPathType(BehaviorType behavior)
        {
            if (behavior == BehaviorType.Customer) return PathType.Customer;
            if (behavior == BehaviorType.Robber) return PathType.Robber;

            throw new System.ArgumentException("振る舞いに対応した経路が無い: " + behavior);
        }
    }
}