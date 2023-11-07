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

        // 振る舞いで分別した中で、更にキャラ毎の辞書が存在する
        Dictionary<BehaviorType, Dictionary<ActorType, Actor>> _table = new();

        void Awake()
        {
            // 振る舞い毎に辞書を作り、その中にキャラ毎に追加していく
            foreach (Actor prefab in _prefabs)
            {
                if (!_table.ContainsKey(prefab.BehaviorType))
                {
                    _table.Add(prefab.BehaviorType, new());
                }

                _table[prefab.BehaviorType].Add(prefab.ActorType, prefab);
            }
        }

        /// <summary>
        /// 振る舞いとキャラクターの種類を指定してキャラクターを生成する
        /// </summary>
        /// <returns>生成したキャラクター</returns>
        public Actor Spawn(BehaviorType behavior, ActorType actor)
        {
            if (_table.TryGetValue(behavior, out Dictionary<ActorType, Actor> dict))
            {
                if (dict.TryGetValue(actor, out Actor prefab))
                {
                    // 適当に画面外に生成する
                    return Instantiate(prefab, new Vector3(100, 100, 100), Quaternion.identity);
                }
                else
                {
                    throw new KeyNotFoundException("キャラの種類に対応したものが生成の辞書に登録されていない: " + actor);
                }
            }
            else
            {
                throw new KeyNotFoundException("振る舞いに対応したものが生成の辞書に登録されていない: " + behavior);
            }
        }
    }
}
