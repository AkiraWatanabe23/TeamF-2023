using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 周囲の既に投げたアイテムを削除する機能のクラス
    /// </summary>
    public class ThrowedItemCleaner : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] ThrowedItemTable _itemTable;

        /// <summary>
        /// このオブジェクトからの水平方向の移動距離が一定以下のアイテムを削除する
        /// </summary>
        public void Clean()
        {
            // 全種類のアイテムに対して処理を行う
            foreach (ThrowedItemHolder holder in _itemTable.GetItemHolderAll())
            {
                List<ThrowedItem> items = holder.Items;
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    // 投げ済み、かつ距離が一定以下
                    if (items[i].IsThrowed && items[i].MovingSqrDistance < _settings.ThrowedAreaSqrRadius)
                    {
                        Destroy(items[i].gameObject);
                        items.RemoveAt(i);
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            DrawCleanRange();
        }

        void DrawCleanRange()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _settings.ThrowedAreaSqrRadius);
        }
    }
}