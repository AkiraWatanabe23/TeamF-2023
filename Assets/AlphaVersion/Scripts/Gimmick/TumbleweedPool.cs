using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Alpha
{
    /// <summary>
    /// �^���u���E�B�[�h�̃v�[�����O���s���N���X
    /// </summary>
    public class TumbleweedPool : ObjectPool<Tumbleweed>
    {
        readonly Tumbleweed[] _origin;
        readonly Transform _parent;

        public TumbleweedPool(string poolName, params Tumbleweed[] origin)
        {
            _origin = origin;
            //_origin.gameObject.SetActive(false);

            _parent = new GameObject(poolName).transform;
        }

        protected override Tumbleweed CreateInstance()
        {
            // �������ɂ��̃v�[���ւ̎Q�Ƃ�n��
            Tumbleweed item = Object.Instantiate(_origin[Random.Range(0, _origin.Length)], _parent);
            item.OnCreate(this);

            return item;
        }

        protected override void OnBeforeRent(Tumbleweed instance)
        {
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(Tumbleweed instance)
        {
            // �v�[���ɖ߂��^�C�~���O�Ŕ�\���ɂ���
            instance.gameObject.SetActive(false);
        }
    }
}
