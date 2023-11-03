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

        Dictionary<ItemType, ThrowedItem> _dict = new();

        void Awake()
        {
            _dict = _data.ToDictionary(d => d.Type, d => d.Prefab);
        }

        /// <summary>
        /// アイテムの種類を指定し、生成して返す
        /// </summary>
        /// <returns>辞書内にある:true ない:false</returns>
        public bool TryCreate(ItemType type, out ThrowedItem item)
        {
            if (_dict.TryGetValue(type, out ThrowedItem prefab))
            {
                return item = Instantiate(prefab);
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItemが辞書内に無い: " + type);
            }
        }
    }
}
