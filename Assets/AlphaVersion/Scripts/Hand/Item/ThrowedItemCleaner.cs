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
        [SerializeField] ThrowedItemHolder _itemHolder;
        [Header("削除する範囲の設定")]
        [SerializeField] float _radius = 1;
        [Header("任意:周囲のアイテム削除を無効化")]
        [SerializeField] bool _invalid;

        /// <summary>
        /// このオブジェクトからの水平方向の移動距離が一定以下のアイテムを削除する
        /// </summary>
        public void Clean()
        {
            // 任意:削除処理を無効化する
            if (_invalid) return;

            List<ThrowedItem> items = _itemHolder.Items;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                // 投げ済み、かつ距離が一定以下
                if (items[i].IsThrowed && items[i].MovingSqrDistance < _radius * _radius)
                {
                    Destroy(items[i].gameObject);
                    items.RemoveAt(i);
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
            Gizmos.DrawWireSphere(transform.position, _radius * _radius);
        }
    }
}