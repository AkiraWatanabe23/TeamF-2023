using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムの生成(インスタンス化)を行うクラス
    /// 生成->初期化->プールに格納の順番で行う
    /// </summary>
    public class ThrowedItemCreator : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public ThrowedItem Prefab;
        }

        [SerializeField] Data[] _data;

        Dictionary<ItemType, ThrowedItemPool> _pools;

        void Awake()
        {
            _pools = _data.ToDictionary(d => d.Type, d => new ThrowedItemPool(d.Prefab, $"ItemPool_{d.Type}"));
        }

        void OnDestroy()
        {
            // 使い終わったプールのDispose
            foreach (KeyValuePair<ItemType, ThrowedItemPool> pair in _pools) pair.Value.Dispose();
        }

        /// <summary>
        /// プールから取り出して返す
        /// </summary>
        /// <returns>辞書内にある:true ない:false</returns>
        public bool TryCreate(ItemType type, out ThrowedItem item)
        {
            if (_pools.TryGetValue(type, out ThrowedItemPool pool))
            {
                return item = pool.Rent();
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItemが辞書内に無い: " + type);
            }
        }
    }
}
