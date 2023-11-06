using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 初期化済みのキャラクターを一覧で保持するクラス
    /// </summary>
    public class ActorSpawnTable : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;

        /// <summary>
        /// 生成したキャラクターを初期化して返す
        /// </summary>
        /// <returns>生成した初期化済みのキャラクター</returns>
        public Actor Initialize(BehaviorType behavior, ActorType actor)
        {
            // このクラスでプールをぶら下げることでプーリングから取り出すクラスにする
            // ぶら下げたプールのクラスから各キャラクターの初期化済みを作る
            // Table->Holder->Init->Create の順

            // TODO:キャラクターのプーリング機能を作る
            return _initializer.Initialize(behavior, actor);
        }
    }
}
