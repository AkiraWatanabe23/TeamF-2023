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
            // 一度生成してから辞書に登録する
            foreach (Data data in _data)
            {
                GameObject go = Instantiate(data.Prefab);
                _dict.Add(data.Order, go);

                go.SetActive(false);
            }
        }

        public void Active(ItemType order, Vector3 position)
        {
            // 現在の注文品を非表示にする
            _dict[_order].SetActive(false);
            // 次の注文を表示にする
            _dict[order].SetActive(true);
            _dict[order].transform.position = position;
            // 注文を更新
            _order = order;
        }

        public void Inactive()
        {
            _dict[_order].SetActive(false);
        }
    }
}
