using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���͂̊��ɓ������A�C�e�����폜����@�\�̃N���X
    /// </summary>
    public class ThrowedItemCleaner : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] ThrowedItemTable _itemTable;

        /// <summary>
        /// ���̃I�u�W�F�N�g����̐��������̈ړ����������ȉ��̃A�C�e�����폜����
        /// </summary>
        public void Clean()
        {
            // �S��ނ̃A�C�e���ɑ΂��ď������s��
            foreach (ThrowedItemHolder holder in _itemTable.GetItemHolderAll())
            {
                List<ThrowedItem> items = holder.Items;
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    // �����ς݁A�����������ȉ�
                    if (items[i].IsThrowed && items[i].MovingSqrDistance < _settings.ThrowedAreaSqrRadius)
                    {
                        Destroy(items[i].gameObject);
                        items.RemoveAt(i);
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            DrawCleanRange();
        }

        void DrawCleanRange()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _settings.ThrowedAreaSqrRadius);
        }
    }
}