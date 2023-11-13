using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 注文に対応する3Dモデルを表示する機能のクラス
    /// 各アイテムに対して1つあれば良いので、予め生成しておき、表示の切り替えを行う。
    /// </summary>
    public class OrderView : MonoBehaviour
    {
        static Transform _parent;

        [System.Serializable]
        public class Data
        {
            public ItemType Order;
            public GameObject Prefab;
        }

        [SerializeField] Data[] _data;

        Dictionary<ItemType, GameObject> _dict = new();
        ItemType _order;

        void Awake()
        {
            // ヒエラルキーを汚さないように共通の親を作り、子にする
            if (_parent == null) _parent = new GameObject("OrderViewParent").transform;

            // 一度生成してから辞書に登録する
            foreach (Data data in _data)
            {
                GameObject go = Instantiate(data.Prefab, _parent);
                _dict.Add(data.Order, go);

                go.SetActive(false);
            }
        }

        void OnDestroy()
        {
            // 共通の親を削除
            if (_parent != null) { Destroy(_parent); _parent = null; }
        }

        public void Active(ItemType order, Vector3 position)
        {
            // 現在の注文品を非表示にする
            _dict[_order].SetActive(false);
            // 次の注文を表示にする
            _dict[order].SetActive(true);
            _dict[order].transform.position = position;
            _dict[order].transform.localScale = Vector3.one;
            // 注文を更新
            _order = order;
        }

        public void Inactive()
        {
            _dict[_order].SetActive(false);
        }
    }
}
