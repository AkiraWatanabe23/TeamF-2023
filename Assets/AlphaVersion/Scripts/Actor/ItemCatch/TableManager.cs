using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �Ȃ��Ǘ��A�q���̃N���X���擾���A�����o�̃C���^�[�t�F�[�X���g�p����
    /// �Ǘ����ƃC���^�[�t�F�[�X�̂����݂̂ŊǗ����邽�߂ɃL�[�ƈꏏ�ɍ���Ă���
    /// </summary>
    public class EmptyTable
    {
        public bool IsEmpty;

        public EmptyTable(Waypoint waypoint, int index, ITableControl table, Vector3 position)
        {
            Waypoint = waypoint;
            Index = index;
            Table = table;
            Position = position;
            IsEmpty = true;
        }

        public Waypoint Waypoint { get; private set; }
        public int Index { get; private set; }
        public ITableControl Table { get; private set; }
        public Vector3 Position { get; private set; }
    }

    /// <summary>
    /// ��Ȃ��擾�A�Ȃ��������@�\�̃N���X
    /// </summary>
    public class TableManager : MonoBehaviour
    {
        /// <summary>
        /// �Ȃƌo�H�̒n�_��R�t����
        /// </summary>
        [System.Serializable]
        public class Data
        {
            public GameObject _table;
            public Waypoint _waypoint;
        }

        [Header("�ȂƑΉ�����n�_")]
        [SerializeField] Data[] _data;

        List<EmptyTable> _emptyTables = new();

        void Awake()
        {
            // �����ɒǉ�
            for (int i = 0; i < _data.Length; i++)
            {
                // �C���^�[�t�F�[�X���������Ă��邩�`�F�b�N
                if (!_data[i]._table.TryGetComponent(out ITableControl control))
                {
                    throw new System.NullReferenceException("ITableControl���������Ă��Ȃ�: " + _data[i]._table);
                }

                _emptyTables.Add(new EmptyTable(_data[i]._waypoint, i, control, _data[i]._table.transform.position));
            }
        }

        /// <summary>
        /// �����_���ȋ�Ȃ��擾����
        /// </summary>
        public bool TryGetEmptyRandom(out EmptyTable emptyTable)
        {
            foreach (EmptyTable t in _emptyTables.OrderBy(_ => System.Guid.NewGuid()))
            {
                if (t.IsEmpty)
                {
                    t.IsEmpty = false;
                    emptyTable = t;
                    return true;
                }
            }

            emptyTable = null;
            return false;
        }

        /// <summary>
        /// �Ȃ�������A�g�p�\�ɂ���
        /// </summary>
        public void Release(EmptyTable emptyTable)
        {
            _emptyTables[emptyTable.Index].IsEmpty = true;
        }
    }
}
