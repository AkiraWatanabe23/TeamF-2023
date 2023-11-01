using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �����ɑΉ�����3D���f����\������@�\�̃N���X
    /// �e�A�C�e���ɑ΂���1����Ηǂ��̂ŁA�\�ߐ������Ă����A�\���̐؂�ւ����s���B
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
            // ��x�������Ă��玫���ɓo�^����
            foreach (Data data in _data)
            {
                GameObject go = Instantiate(data.Prefab);
                _dict.Add(data.Order, go);

                go.SetActive(false);
            }
        }

        public void Active(ItemType order, Vector3 position)
        {
            // ���݂̒����i���\���ɂ���
            _dict[_order].SetActive(false);
            // ���̒�����\���ɂ���
            _dict[order].SetActive(true);
            _dict[order].transform.position = position;
            // �������X�V
            _order = order;
        }

        public void Inactive()
        {
            _dict[_order].SetActive(false);
        }
    }
}
