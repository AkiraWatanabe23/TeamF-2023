using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムをプールから生成/戻す機能のクラス
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemTable _itemTable;

        /// <summary>
        /// プールから取り出す
        /// </summary>
        /// <returns>生成済みのアイテム</returns>
        public ThrowedItem Spawn(ItemType item)
        {
            if (_itemTable.TryGetItemHolder(item, out ThrowedItemHolder holder))
            {
                return holder.PopItem();
            }
            else
            {
                throw new KeyNotFoundException("辞書に投げるアイテムが登録されていない: " + item);
            }
        }

        /// <summary>
        /// 不要になった際にプールに戻す
        /// </summary>
        public void Release(ThrowedItem item)
        {
            // TODO:プーリングする際はプールに戻す処理に修正する
            //Destroy(item.gameObject);
            item.Catch();
        }
    }
}
