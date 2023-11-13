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
        // �U�镑���ŕ��ʂ������ŁA�X�ɃL�������̎��������݂���
        Dictionary<BehaviorType, Dictionary<ActorType, Actor>> _table = new();

        void Awake()
        {
            // �U�镑�����Ɏ��������A���̒��ɃL�������ɒǉ����Ă���
            foreach (Actor prefab in _prefabs)
            {
                if (!_table.ContainsKey(prefab.BehaviorType))
                {
                    _table.Add(prefab.BehaviorType, new());
                }

                _table[prefab.BehaviorType].Add(prefab.ActorType, prefab);
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
            if (_table.TryGetValue(behavior, out Dictionary<ActorType, Actor> dict))
            {
                if (dict.TryGetValue(actor, out Actor prefab))
                {
                    // TODO:�L�����N�^�[�̃v�[�����O�B���󂱂̃N���X�ɓo�^����e���������Ă���B
                    
                    // �K���ɉ�ʊO�ɐ�������
                    return Instantiate(prefab, new Vector3(100, 100, 100), Quaternion.identity, _parent);
                }
                else
                {
                    throw new KeyNotFoundException("�L�����̎�ނɑΉ��������̂������̎����ɓo�^����Ă��Ȃ�: " + actor);
                }
            }
            else
            {
                throw new KeyNotFoundException("�U�镑���ɑΉ��������̂������̎����ɓo�^����Ă��Ȃ�: " + behavior);
            }
        }
    }
}
