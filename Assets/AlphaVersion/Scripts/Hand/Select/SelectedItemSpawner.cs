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
            return _spawner.Spawn(_selectedItem);
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
            _selectedItem = _selector.Select(key);
            
            // テムの種類をメッセージングする、音を出す
        }

        /// <summary>
        /// マウスホイールで選択を行う
        /// </summary>
        public void Select(float fov)
        {
            _selectedItem = _selector.Select(fov);

            // テムの種類をメッセージングする、音を出す
        }
    }
}