using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムのインスタンスを保持するクラス
    /// アイテムの各種操作を行う場合はこのクラスを参照する
    /// </summary>
    public class ThrowedItemHolder : MonoBehaviour
    {
        [SerializeField] ItemType _type;
        [SerializeField] ThrowedItemInitializer _initializer;

        List<ThrowedItem> _items = new();

        // NOTE:外部から中身を弄れる
        public List<ThrowedItem> Items => _items;

        /// <summary>
        /// 生成済みのアイテムを取得する
        /// </summary>
        public ThrowedItem PopItem()
        {
            // TODO:本来ならプーリング
            ThrowedItem item = _initializer.Initialize(_type);
            Items.Add(item);

            return item;
        }
    }
}
