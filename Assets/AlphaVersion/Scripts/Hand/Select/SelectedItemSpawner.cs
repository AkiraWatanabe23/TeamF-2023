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
            return _spawner.Spawn(_selectedItem);
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
            _selectedItem = _selector.Select(key);
            
            // �e���̎�ނ����b�Z�[�W���O����A�����o��
        }

        /// <summary>
        /// �}�E�X�z�C�[���őI�����s��
        /// </summary>
        public void Select(float fov)
        {
            _selectedItem = _selector.Select(fov);

            // �e���̎�ނ����b�Z�[�W���O����A�����o��
        }
    }
}