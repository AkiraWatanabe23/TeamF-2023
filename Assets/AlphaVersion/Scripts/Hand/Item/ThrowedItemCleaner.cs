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
        [SerializeField] ThrowedItemHolder _itemHolder;
        [Header("�폜����͈͂̐ݒ�")]
        [SerializeField] float _radius = 1;
        [Header("�C��:���͂̃A�C�e���폜�𖳌���")]
        [SerializeField] bool _invalid;

        /// <summary>
        /// ���̃I�u�W�F�N�g����̐��������̈ړ����������ȉ��̃A�C�e�����폜����
        /// </summary>
        public void Clean()
        {
            // �C��:�폜�����𖳌�������
            if (_invalid) return;

            List<ThrowedItem> items = _itemHolder.Items;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                // �����ς݁A�����������ȉ�
                if (items[i].IsThrowed && items[i].MovingSqrDistance < _radius * _radius)
                {
                    Destroy(items[i].gameObject);
                    items.RemoveAt(i);
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
            Gizmos.DrawWireSphere(transform.position, _radius * _radius);
        }
    }
}