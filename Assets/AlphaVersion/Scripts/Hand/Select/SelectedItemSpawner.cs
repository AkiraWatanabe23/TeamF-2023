using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �}�E�X�z�C�[���������̓L�[���͂ŁA������A�C�e����I�����A
    /// �I�������A�C�e���𐶐�/�j������N���X�B
    /// </summary>
    public class SelectedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemSpawner _spawner;
        [SerializeField] FerverItemSelector _ferverSelector;

        ItemSelector _selector = new();
        ItemType _selectedItem;

        void Start()
        {
            // �����l�Ƃ���A�L�[�ŃA�C�e����I������
            Select(KeyCode.A);
        }

        /// <summary>
        /// �I�������A�C�e�����v�[��������o��
        /// </summary>
        /// <returns>�����ς݂̃A�C�e��</returns>
        public ThrowedItem Spawn()
        {
            ItemType item = _ferverSelector.Select(_selectedItem);
            return _spawner.Spawn(item);
        }

        /// <summary>
        /// �A�C�e�����v�[���ɖ߂�
        /// </summary>
        public void Release(ThrowedItem item)
        {
            _spawner.Release(item);
        }

        /// <summary>
        /// �L�[���͂őI�����s��
        /// </summary>
        public void Select(KeyCode key)
        {
            _selectedItem = SelectWithPlaySE(_selector.Select(key));
        }

        /// <summary>
        /// �}�E�X�z�C�[���őI�����s��
        /// </summary>
        public void Select(float fov)
        {
            _selectedItem = SelectWithPlaySE(_selector.Select(fov));
        }

        /// <summary>
        /// �I��������SE�̍Đ�������
        /// </summary>
        ItemType SelectWithPlaySE(ItemType next)
        {
            if (_selectedItem != next) Cri.PlaySE("SE_ItemSelect_2");
            return next;
        }
    }
}