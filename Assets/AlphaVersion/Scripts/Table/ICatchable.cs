using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 客がキャッチ可能なアイテムのインターフェース
    /// 注文したアイテムかつ、速度が一定以下の場合はキャッチされる
    /// </summary>
    public interface ICatchable
    {
        /// <summary>
        /// 注文したアイテムかどうかを判定するのに使用する
        /// </summary>
        public ItemType Type { get; }

        /// <summary>
        /// キャッチエリアがこのアイテムをキャッチしたかを判定するために使用する
        /// </summary>
        public float SqrSpeed { get; }

        /// <summary>
        /// キャッチした際に呼ばれる
        /// </summary>
        public void OnCatched();
    }
}
