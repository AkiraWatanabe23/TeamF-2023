using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Alpha
{
    /// <summary>
    /// パーティクルをプーリングするためのプール
    /// </summary>
    public class ParticlePool : ObjectPool<Particle>
    {
        readonly Particle _origin;
        readonly Transform _parent;

        public ParticlePool(Particle origin, string poolName)
        {
            _origin = origin;
            _origin.gameObject.SetActive(false);

            _parent = new GameObject(poolName).transform;
        }

        protected override Particle CreateInstance()
        {
            // 生成時にこのプールへの参照を渡す
            Particle item = Object.Instantiate(_origin, _parent);
            item.OnCreate(this);

            return item;
        }

        protected override void OnBeforeRent(Particle instance)
        {
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(Particle instance)
        {
            // プールに戻すタイミングで親を設定する
            instance.transform.parent = _parent;
        }
    }
}

