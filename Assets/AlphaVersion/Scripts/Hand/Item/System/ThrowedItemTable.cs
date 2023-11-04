using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 各アイテムを保持しているThrowedItemHolderをまとめて保持するクラス
    /// </summary>
    public class ThrowedItemTable : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public ThrowedItemHolder Holder;
        }

        [SerializeField] Data[] _data;

        Dictionary<ItemType, ThrowedItemHolder> _dict = new();

        void Awake()
        {
            _dict = _data.ToDictionary(d => d.Type, d => d.Holder);
        }

        /// <summary>
        /// アイテムの種類を指定し、対応したThrowedItemHolderを返す
        /// </summary>
        /// <returns>辞書内にある:true ない:false</returns>
        public bool TryGetItemHolder(ItemType type, out ThrowedItemHolder holder)
        {
            if (_dict.TryGetValue(type, out holder))
            {
                return holder;
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItemHolderが辞書内に無い: " + type);
            }
        }

        /// <summary>
        /// このクラスの辞書に追加されている全てのThrowedItemHolderを返す
        /// </summary>
        /// <returns>辞書の値として追加されている全てのThrowedItemHolder</returns>
        public IEnumerable<ThrowedItemHolder> GetItemHolderAll()
        {
            return _dict.Values;
        }
    }
}
