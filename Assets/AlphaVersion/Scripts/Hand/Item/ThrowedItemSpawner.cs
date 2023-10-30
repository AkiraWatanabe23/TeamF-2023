using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    // 現状、プールが1つしかないのでこのクラスがある意味が無いが
    // アイテムの種類が増えた場合は、このクラスにどのアイテムを生成するかを渡して
    // 対応するプールから取り出してもらう、仲介役？

    /// <summary>
    /// 投げるアイテムをプールから生成/戻す機能のクラス
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemHolder _itemHolder;

        /// <summary>
        /// プールから取り出す
        /// </summary>
        /// <returns>生成済みのアイテム</returns>
        public ThrowedItem Spawn(ItemType item)
        {
            return _itemHolder.PopItem();
        }

        /// <summary>
        /// 不要になった際にプールに戻す
        /// </summary>
        public void Release(ThrowedItem item)
        {
            // TODO:プーリングする際はプールに戻す処理に修正する
            Destroy(item.gameObject); ;
        }
    }
}
