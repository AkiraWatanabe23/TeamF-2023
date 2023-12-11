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
        [Header("キャラクターのパラメータのSOを全部")]
        [SerializeField] ActorSettingsSO _femaleSettings;
        [SerializeField] ActorSettingsSO _femaleOnkouSettings;
        [SerializeField] ActorSettingsSO _femaleTankiSettings;
        [SerializeField] ActorSettingsSO _maleSettings;
        [SerializeField] ActorSettingsSO _maleOnkouSettings;
        [SerializeField] ActorSettingsSO _maleTankiSettings;
        [SerializeField] ActorSettingsSO _robberSettings;
        [Header("このステージで注文可能(Hand側と合わせる)")]
        [SerializeField] ItemType[] _orders;
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

            instance._settings = GetSettings(instance.Character);

            if (instance.TryGetComponent(out MoveState moveState))
            {
                moveState._settings = GetSettings(instance.Character);
            }

            // 客の場合はOrderStateを持っているので生成したタイミングでどの注文を頼めるかを渡す
            if (instance.TryGetComponent(out OrderState orderState))
            {
                orderState.Orders = _orders;
                orderState._settings = GetSettings(instance.Character);
            }

            if (instance.TryGetComponent(out ResultState resultState))
            {
                resultState._settings = GetSettings(instance.Character);
            }

            // 客の場合は、経路と席と現在フィーバータイムかどうかを渡す
            if (behavior == BehaviorType.Customer) instance.Init(lead, Tension, _tableManager);
            if (behavior == BehaviorType.Robber) instance.Init(lead, Tension);

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

        ActorSettingsSO GetSettings(CharacterType character)
        {
            if (character == CharacterType.Female) return _femaleSettings;
            else if (character == CharacterType.FemaleOnkou) return _femaleOnkouSettings;
            else if (character == CharacterType.FemaleTanki) return _femaleTankiSettings;
            else if (character == CharacterType.Male) return _maleSettings;
            else if (character == CharacterType.MaleOnkou) return _maleOnkouSettings;
            else if (character == CharacterType.MaleTanki) return _maleTankiSettings;
            else return _robberSettings;
        }
    }
}