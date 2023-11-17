using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// マウスホイールもしくはキー入力で、投げるアイテムを選択し、
    /// 選択したアイテムを生成/破棄するクラス。
    /// </summary>
    public class SelectedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemSpawner _spawner;
        [SerializeField] FerverItemSelector _ferverSelector;

        ItemSelector _selector = new();
        ItemType _selectedItem;

        void Start()
        {
            // 初期値としてAキーでアイテムを選択する
            Select(KeyCode.A);
        }

        /// <summary>
        /// 選択したアイテムをプールから取り出す
        /// </summary>
        /// <returns>生成済みのアイテム</returns>
        public ThrowedItem Spawn()
        {
            ItemType item = _ferverSelector.Select(_selectedItem);
            return _spawner.Spawn(item);
        }

        /// <summary>
        /// アイテムをプールに戻す
        /// </summary>
        public void Release(ThrowedItem item)
        {
            _spawner.Release(item);
        }

        /// <summary>
        /// キー入力で選択を行う
        /// </summary>
        public void Select(KeyCode key)
        {
            _selectedItem = SelectWithPlaySE(_selector.Select(key));
        }

        /// <summary>
        /// マウスホイールで選択を行う
        /// </summary>
        public void Select(float fov)
        {
            _selectedItem = SelectWithPlaySE(_selector.Select(fov));
        }

        /// <summary>
        /// 選択処理にSEの再生を挟む
        /// </summary>
        ItemType SelectWithPlaySE(ItemType next)
        {
            if (_selectedItem != next) Cri.PlaySE("SE_ItemSelect_2");
            return next;
        }
    }
}