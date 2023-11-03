using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 生成したキャラクターの初期化を行うクラス
    /// ManagerとSpawnerの間で、処理を挟んで橋渡しを行う
    /// </summary>
    public class ActorInitializer : MonoBehaviour
    {
        [SerializeField] ActorSpawner _spawner;
        [Header("初期化に必要なもの")]
        [SerializeField] PathCreator _pathCreator;

        /// <summary>
        /// 生成したキャラクターを初期化して返す
        /// </summary>
        /// <returns>生成した初期化済みのキャラクター</returns>
        public Actor Initialize(BehaviorType behavior)
        {
            Actor actor = _spawner.Spawn(behavior);
            Waypoint lead = _pathCreator.GetPath(ToPathType(behavior));
            actor.Init(lead);

            return actor;
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

// ↓次やること
// キャラクターに経路を渡したい
// 生成クラスは生成のみを行いこのクラスに返ってくる
// 経路を生成するクラスからどうやって渡すか
// 経路を渡すインターフェースを作成して渡す案
// object型を渡すインターフェースを作成して渡す案