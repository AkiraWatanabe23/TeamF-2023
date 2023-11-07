using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 席を管理、客このクラスを取得し、メンバのインターフェースを使用する
    /// 管理側とインターフェースのやり取りのみで管理するためにキーと一緒に梱包している
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
    /// 空席を取得、席を解放する機能のクラス
    /// </summary>
    public class TableManager : MonoBehaviour
    {
        /// <summary>
        /// 席と経路の地点を紐付ける
        /// </summary>
        [System.Serializable]
        public class Data
        {
            public GameObject _table;
            public Waypoint _waypoint;
        }

        [Header("席と対応する地点")]
        [SerializeField] Data[] _data;

        List<EmptyTable> _emptyTables = new();

        void Awake()
        {
            // 辞書に追加
            for (int i = 0; i < _data.Length; i++)
            {
                // インターフェースを実装しているかチェック
                if (!_data[i]._table.TryGetComponent(out ITableControl control))
                {
                    throw new System.NullReferenceException("ITableControlを実装していない: " + _data[i]._table);
                }

                _emptyTables.Add(new EmptyTable(_data[i]._waypoint, i, control, _data[i]._table.transform.position));
            }
        }

        /// <summary>
        /// ランダムな空席を取得する
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
        /// 席を解放し、使用可能にする
        /// </summary>
        public void Release(EmptyTable emptyTable)
        {
            _emptyTables[emptyTable.Index].IsEmpty = true;
        }
    }
}
