using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e�����v�[�����琶��/�߂��@�\�̃N���X
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemTable _itemTable;

        /// <summary>
        /// �v�[��������o��
        /// </summary>
        /// <returns>�����ς݂̃A�C�e��</returns>
        public ThrowedItem Spawn(ItemType item)
        {
            if (_itemTable.TryGetItemHolder(item, out ThrowedItemHolder holder))
            {
                return holder.PopItem();
            }
            else
            {
                throw new KeyNotFoundException("�����ɓ�����A�C�e�����o�^����Ă��Ȃ�: " + item);
            }
        }

        /// <summary>
        /// �s�v�ɂȂ����ۂɃv�[���ɖ߂�
        /// </summary>
        public void Release(ThrowedItem item)
        {
            // TODO:�v�[�����O����ۂ̓v�[���ɖ߂������ɏC������
            //Destroy(item.gameObject);
            item.Catch();
        }
    }
}
