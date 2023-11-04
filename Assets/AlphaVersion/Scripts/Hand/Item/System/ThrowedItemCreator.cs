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

        Dictionary<ItemType, ThrowedItem> _dict = new();

        void Awake()
        {
            _dict = _data.ToDictionary(d => d.Type, d => d.Prefab);
        }

        /// <summary>
        /// �A�C�e���̎�ނ��w�肵�A�������ĕԂ�
        /// </summary>
        /// <returns>�������ɂ���:true �Ȃ�:false</returns>
        public bool TryCreate(ItemType type, out ThrowedItem item)
        {
            if (_dict.TryGetValue(type, out ThrowedItem prefab))
            {
                return item = Instantiate(prefab);
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItem���������ɖ���: " + type);
            }
        }
    }
}
