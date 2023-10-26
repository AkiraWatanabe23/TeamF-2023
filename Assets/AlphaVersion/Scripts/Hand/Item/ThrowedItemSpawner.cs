using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムを生成する機能のクラス
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemHolder _itemHolder;

        public ThrowedItem Spawn()
        {
            return _itemHolder.PopItem();
        }
    }
}
