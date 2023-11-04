using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���̃C���X�^���X��ێ�����N���X
    /// �A�C�e���̊e�푀����s���ꍇ�͂��̃N���X���Q�Ƃ���
    /// </summary>
    public class ThrowedItemHolder : MonoBehaviour
    {
        [SerializeField] ItemType _type;
        [SerializeField] ThrowedItemInitializer _initializer;

        List<ThrowedItem> _items = new();

        // NOTE:�O�����璆�g��M���
        public List<ThrowedItem> Items => _items;

        /// <summary>
        /// �����ς݂̃A�C�e�����擾����
        /// </summary>
        public ThrowedItem PopItem()
        {
            // TODO:�{���Ȃ�v�[�����O
            ThrowedItem item = _initializer.Initialize(_type);
            Items.Add(item);

            return item;
        }
    }
}
