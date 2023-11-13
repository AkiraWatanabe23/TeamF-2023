using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Alpha
{
    /// <summary>
    /// �p�[�e�B�N�����v�[�����O���邽�߂̃v�[��
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
            // �������ɂ��̃v�[���ւ̎Q�Ƃ�n��
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
            // �v�[���ɖ߂��^�C�~���O�Őe��ݒ肷��
            instance.transform.parent = _parent;
        }
    }
}

