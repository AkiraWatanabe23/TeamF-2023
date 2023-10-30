using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Alpha
{
    /// <summary>
    /// 入力に対応したアイテムを選択する機能のクラス
    /// </summary>
    public class ItemSelector
    {
        int _currentIndex;
        
        int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                
                // アイテムの数の範囲にクランプする
                int length = Enum.GetValues(typeof(ItemType)).Length;
                _currentIndex = Mathf.Clamp(_currentIndex, 0, length - 1);

                // 選択したアイテムをメッセージングする
                ItemMessageSender.SendMessage((ItemType)CurrentIndex);
            }
        }

        /// <summary>
        /// キー入力で選択を行う
        /// 対応したキー(ASDFG)の場合のみ変更し、それ以外の場合は現在の添え字を維持する
        /// </summary>
        public ItemType Select(KeyCode key)
        {
            // TODO:キーを添え字に対応させているだけなので、アイテムの種類が増減した場合は修正が必要
            if      (key == KeyCode.A) CurrentIndex = 0;
            else if (key == KeyCode.S) CurrentIndex = 1;
            else if (key == KeyCode.D) CurrentIndex = 2;
            else if (key == KeyCode.F) CurrentIndex = 3;
            else if (key == KeyCode.G) CurrentIndex = 4;

            return (ItemType)CurrentIndex;
        }

        /// <summary>
        /// マウスホイールで選択を行う
        /// </summary>
        public ItemType Select(float fov)
        {
            if (fov > 0) CurrentIndex--;
            if (fov < 0) CurrentIndex++;

            return (ItemType)CurrentIndex;
        }
    }
}

