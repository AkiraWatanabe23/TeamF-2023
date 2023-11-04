using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// キャラクターの生成を行うクラス
    /// 生成 -> 初期化 -> プールへの格納 の手順のうち、生成を担当
    /// </summary>
    public class ActorSpawner : MonoBehaviour
    {
        [SerializeField] Actor[] _prefabs;

        Dictionary<BehaviorType, Actor> _behaviorDict = new();

        void Awake()
        {
            // 辞書に追加
            foreach (Actor prefab in _prefabs)
            {
                _behaviorDict.Add(prefab.BehaviorType, prefab);
            }
        }

        /// <summary>
        /// 振る舞いを指定してキャラクターを生成する
        /// </summary>
        /// <returns>生成したキャラクター</returns>
        public Actor Spawn(BehaviorType behavior)
        {
            Actor actor = Instantiate(_behaviorDict[behavior]);
            return actor;
        }
    }
}
