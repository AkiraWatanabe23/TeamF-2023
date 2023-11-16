using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// フィーバタイムの際は投げるアイテムをランダムにミニキャラクターにするクラス
    /// </summary>
    public class FerverItemSelector : FerverHandler
    {
        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// フィーバータイム中はランダムでミニキャラを選択する
        /// </summary>
        public ItemType Select(ItemType item)
        {
            if (Tension == Tension.Normal) return item;

            return Random.value < _settings.FerverMiniActorPercent ? ItemType.MiniActor : item;
        }
    }
}