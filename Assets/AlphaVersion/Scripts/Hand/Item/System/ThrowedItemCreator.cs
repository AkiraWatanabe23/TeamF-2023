using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���̐���(�C���X�^���X��)���s���N���X
    /// ����->������->�v�[���Ɋi�[�̏��Ԃōs��
    /// </summary>
    public class ThrowedItemCreator : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public ThrowedItem Prefab;
        }

        [SerializeField] Data[] _data;
        [SerializeField] ThrowedItem[] _fevers;

        Dictionary<ItemType, ThrowedItemPool> _pools;
        List<ThrowedItemPool> _feverPools = new();

        void Awake()
        {
            _pools = _data.ToDictionary(d => d.Type, d => new ThrowedItemPool(d.Prefab, $"ItemPool_{d.Type}"));

            foreach (ThrowedItem item in _fevers)
            {
                _feverPools.Add(new(item, $"ItemPool_{item.name}"));
            }
        }

        void OnDestroy()
        {
            // �g���I������v�[����Dispose
            foreach (KeyValuePair<ItemType, ThrowedItemPool> pair in _pools) pair.Value.Dispose();
            foreach (ThrowedItemPool pool in _feverPools) pool.Dispose();
        }

        /// <summary>
        /// �v�[��������o���ĕԂ�
        /// </summary>
        /// <returns>�������ɂ���:true �Ȃ�:false</returns>
        public bool TryCreate(ItemType type, out ThrowedItem item)
        {
            if (type == ItemType.MiniActor)
            {
                item = _feverPools[Random.Range(0, _feverPools.Count)].Rent();
                return true;
            }

            if (_pools.TryGetValue(type, out ThrowedItemPool pool))
            {
                return item = pool.Rent();
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItem���������ɖ���: " + type);
            }
        }
    }
}
