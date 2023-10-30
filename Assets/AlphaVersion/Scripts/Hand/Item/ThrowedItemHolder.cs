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
        [SerializeField] ThrowedItem _prefab;

        List<ThrowedItem> _items = new();

        // NOTE:�O�����璆�g��M���
        public List<ThrowedItem> Items => _items;

        /// <summary>
        /// �����ς݂̃A�C�e�����擾����
        /// </summary>
        public ThrowedItem PopItem()
        {
            // TODO:�{���Ȃ�v�[�����O
            ThrowedItem item = Instantiate(_prefab);
            Items.Add(item);

            return item;
        }
    }
}
