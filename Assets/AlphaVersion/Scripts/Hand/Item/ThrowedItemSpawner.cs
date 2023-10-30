using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    // ����A�v�[����1�����Ȃ��̂ł��̃N���X������Ӗ���������
    // �A�C�e���̎�ނ��������ꍇ�́A���̃N���X�ɂǂ̃A�C�e���𐶐����邩��n����
    // �Ή�����v�[��������o���Ă��炤�A������H

    /// <summary>
    /// ������A�C�e�����v�[�����琶��/�߂��@�\�̃N���X
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemHolder _itemHolder;

        /// <summary>
        /// �v�[��������o��
        /// </summary>
        /// <returns>�����ς݂̃A�C�e��</returns>
        public ThrowedItem Spawn()
        {
            return _itemHolder.PopItem();
        }

        /// <summary>
        /// �s�v�ɂȂ����ۂɃv�[���ɖ߂�
        /// </summary>
        public void Release(ThrowedItem item)
        {
            // TODO:���݂͐������Ă���̂ō폜����
            Destroy(item.gameObject); ;
        }
    }
}
