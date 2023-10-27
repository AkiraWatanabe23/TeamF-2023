using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���𐶐�����@�\�̃N���X
    /// </summary>
    public class ThrowedItemSpawner : MonoBehaviour
    {
        [SerializeField] ThrowedItemHolder _itemHolder;

        public ThrowedItem Spawn()
        {
            return _itemHolder.PopItem();
        }
    }
}
