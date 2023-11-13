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
            // �q�G�����L�[�������Ȃ��悤�ɋ��ʂ̐e�����A�q�ɂ���
            if (_parent == null) _parent = new GameObject("OrderViewParent").transform;

            // ��x�������Ă��玫���ɓo�^����
            foreach (Data data in _data)
            {
                GameObject go = Instantiate(data.Prefab, _parent);
                _dict.Add(data.Order, go);

                go.SetActive(false);
            }
        }

        void OnDestroy()
        {
            // ���ʂ̐e���폜
            if (_parent != null) { Destroy(_parent); _parent = null; }
        }

        public void Active(ItemType order, Vector3 position)
        {
            // ���݂̒����i���\���ɂ���
            _dict[_order].SetActive(false);
            // ���̒�����\���ɂ���
            _dict[order].SetActive(true);
            _dict[order].transform.position = position;
            _dict[order].transform.localScale = Vector3.one;
            // �������X�V
            _order = order;
        }

        public void Inactive()
        {
            _dict[_order].SetActive(false);
        }
    }
}
