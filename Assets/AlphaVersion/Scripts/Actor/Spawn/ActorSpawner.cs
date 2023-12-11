using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Alpha
{
    /// <summary>
    /// キャラクターの生成を行うクラス
    /// 生成 -> 初期化 -> プールへの格納 の手順のうち、生成を担当
    /// </summary>
    public class ActorSpawner : MonoBehaviour
    {
        [SerializeField] Actor[] _prefabs;

        Transform _parent;
        // 強盗と客でそれぞれリスト
        List<Actor> _customers = new();
        List<Actor> _robbers = new();

        void Awake()
        {
            foreach (Actor actor in _prefabs)
            {
                if (actor.ActorType == ActorType.Male || actor.ActorType == ActorType.Female)
                {
                    _customers.Add(actor);
                }
                if (actor.ActorType == ActorType.Muscle || actor.ActorType == ActorType.Robber)
                {
                    _robbers.Add(actor);
                }
            }

            // 生成したキャラクターを登録する親
            _parent = new GameObject("ActorPool").transform;
        }

        /// <summary>
        /// 振る舞いとキャラクターの種類を指定してキャラクターを生成する
        /// </summary>
        /// <returns>生成したキャラクター</returns>
        public Actor Spawn(BehaviorType behavior, ActorType actor)
        {
            if (behavior == BehaviorType.Customer)
            {
                Actor a = _customers[Random.Range(0, _customers.Count)];
                return Instantiate(a, new Vector3(100, 100, 100), Quaternion.identity, _parent);
            }
            else
            {
                Actor a = _robbers[Random.Range(0, _robbers.Count)];
                return Instantiate(a, new Vector3(100, 100, 100), Quaternion.identity, _parent);
            }
        }
    }
}
