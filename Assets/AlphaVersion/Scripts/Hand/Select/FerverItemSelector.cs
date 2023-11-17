using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�^�C���̍ۂ͓�����A�C�e���������_���Ƀ~�j�L�����N�^�[�ɂ���N���X
    /// </summary>
    public class FerverItemSelector : FerverHandler
    {
        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// �t�B�[�o�[�^�C�����̓����_���Ń~�j�L������I������
        /// </summary>
        public ItemType Select(ItemType item)
        {
            if (Tension == Tension.Normal) return item;

            return Random.value < _settings.FerverMiniActorPercent ? ItemType.MiniActor : item;
        }
    }
}