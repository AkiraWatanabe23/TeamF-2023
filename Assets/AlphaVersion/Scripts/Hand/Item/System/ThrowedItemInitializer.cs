using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���̏��������s���N���X
    /// ����->������->�v�[���Ɋi�[�̏��Ԃōs��
    /// </summary>
    public class ThrowedItemInitializer : MonoBehaviour
    {
        [SerializeField] ThrowedItemCreator _creator;
        [Header("���������ɓn������")]
        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// �������A���������\�b�h���Ăяo���ĕK�v�Ȃ��̂�n���A�Ԃ��B
        /// </summary>
        /// <returns>�������ς݂̐��������A�C�e��</returns>
        public ThrowedItem Initialize(ItemType type)
        {
            _creator.TryCreate(type, out ThrowedItem item);
            item.Init(_settings);
            return item;
        }
    }
}
