using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �e�A�C�e����ێ����Ă���ThrowedItemHolder���܂Ƃ߂ĕێ�����N���X
    /// </summary>
    public class ThrowedItemTable : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public ThrowedItemHolder Holder;
        }

        [SerializeField] Data[] _data;

        Dictionary<ItemType, ThrowedItemHolder> _dict = new();

        void Awake()
        {
            _dict = _data.ToDictionary(d => d.Type, d => d.Holder);
        }

        /// <summary>
        /// �A�C�e���̎�ނ��w�肵�A�Ή�����ThrowedItemHolder��Ԃ�
        /// </summary>
        /// <returns>�������ɂ���:true �Ȃ�:false</returns>
        public bool TryGetItemHolder(ItemType type, out ThrowedItemHolder holder)
        {
            if (_dict.TryGetValue(type, out holder))
            {
                return holder;
            }
            else
            {
                throw new KeyNotFoundException("ThrowedItemHolder���������ɖ���: " + type);
            }
        }

        /// <summary>
        /// ���̃N���X�̎����ɒǉ�����Ă���S�Ă�ThrowedItemHolder��Ԃ�
        /// </summary>
        /// <returns>�����̒l�Ƃ��Ēǉ�����Ă���S�Ă�ThrowedItemHolder</returns>
        public IEnumerable<ThrowedItemHolder> GetItemHolderAll()
        {
            return _dict.Values;
        }
    }
}
