using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Alpha
{
    /// <summary>
    /// �L�����N�^�[�̐������s���N���X
    /// ���� -> ������ -> �v�[���ւ̊i�[ �̎菇�̂����A������S��
    /// </summary>
    public class ActorSpawner : MonoBehaviour
    {
        [SerializeField] Actor[] _prefabs;

        Transform _parent;
        // �����Ƌq�ł��ꂼ�ꃊ�X�g
        List<Actor> _customers = new();
        List<Actor> _robbers = new();

        void Awake()
        {
            foreach (Actor actor in _prefabs)
            {
                if (actor.ActorType == ActorType.Male || actor.ActorType == ActorType.Female)
                {
                    _customers.Add(actor);
                }
                if (actor.ActorType == ActorType.Muscle || actor.ActorType == ActorType.Robber)
                {
                    _robbers.Add(actor);
                }
            }

            // ���������L�����N�^�[��o�^����e
            _parent = new GameObject("ActorPool").transform;
        }

        /// <summary>
        /// �U�镑���ƃL�����N�^�[�̎�ނ��w�肵�ăL�����N�^�[�𐶐�����
        /// </summary>
        /// <returns>���������L�����N�^�[</returns>
        public Actor Spawn(BehaviorType behavior, ActorType actor)
        {
            if (behavior == BehaviorType.Customer)
            {
                Actor a = _customers[Random.Range(0, _customers.Count)];
                return Instantiate(a, new Vector3(100, 100, 100), Quaternion.identity, _parent);
            }
            else
            {
                Actor a = _robbers[Random.Range(0, _robbers.Count)];
                return Instantiate(a, new Vector3(100, 100, 100), Quaternion.identity, _parent);
            }
        }
    }
}
