using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���̏��������s���N���X
    /// ����->������->�v�[���Ɋi�[�̏��Ԃōs��
    /// </summary>
    public class ThrowedItemInitializer : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public ItemSettingsSO Settings;
        }

        [SerializeField] ThrowedItemCreator _creator;
        [Header("���������ɓn������")]
        [SerializeField] Data[] _settingsData;

        Dictionary<ItemType, ItemSettingsSO> _settingsDict;

        void Awake()
        {
            _settingsDict = _settingsData.ToDictionary(d => d.Type, d => d.Settings);
        }

        /// <summary>
        /// �������A���������\�b�h���Ăяo���ĕK�v�Ȃ��̂�n���A�Ԃ��B
        /// </summary>
        /// <returns>�������ς݂̐��������A�C�e��</returns>
        public ThrowedItem Initialize(ItemType type)
        {
            _creator.TryCreate(type, out ThrowedItem item);
            item.Init(_settingsDict[type]);
            return item;
        }
    }
}
