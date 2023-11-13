using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムをプーリングするためのプール
    /// </summary>
    public class ThrowedItemPool : ObjectPool<ThrowedItem>
    {
        readonly ThrowedItem _origin;
        readonly Transform _parent;

        public ThrowedItemPool(ThrowedItem origin, string poolName)
        {
            _origin = origin;
            _origin.gameObject.SetActive(false);

            _parent = new GameObject(poolName).transform;
        }

        protected override ThrowedItem CreateInstance()
        {
            // 生成時にこのプールへの参照を渡す
            ThrowedItem item = Object.Instantiate(_origin, _parent);
            item.OnCreate(this);

            return item;
        }

        protected override void OnBeforeRent(ThrowedItem instance)
        {
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(ThrowedItem instance)
        {
            base.OnBeforeReturn(instance);
        }
    }
}
