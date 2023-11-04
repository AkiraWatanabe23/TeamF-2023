using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムの初期化を行うクラス
    /// 生成->初期化->プールに格納の順番で行う
    /// </summary>
    public class ThrowedItemInitializer : MonoBehaviour
    {
        [SerializeField] ThrowedItemCreator _creator;
        [Header("初期化時に渡すもの")]
        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// 生成し、初期化メソッドを呼び出して必要なものを渡し、返す。
        /// </summary>
        /// <returns>初期化済みの生成したアイテム</returns>
        public ThrowedItem Initialize(ItemType type)
        {
            _creator.TryCreate(type, out ThrowedItem item);
            item.Init(_settings);
            return item;
        }
    }
}
