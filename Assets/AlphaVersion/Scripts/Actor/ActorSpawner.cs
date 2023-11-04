using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �L�����N�^�[�̐������s���N���X
    /// ���� -> ������ -> �v�[���ւ̊i�[ �̎菇�̂����A������S��
    /// </summary>
    public class ActorSpawner : MonoBehaviour
    {
        [SerializeField] Actor[] _prefabs;

        Dictionary<BehaviorType, Actor> _behaviorDict = new();

        void Awake()
        {
            // �����ɒǉ�
            foreach (Actor prefab in _prefabs)
            {
                _behaviorDict.Add(prefab.BehaviorType, prefab);
            }
        }

        /// <summary>
        /// �U�镑�����w�肵�ăL�����N�^�[�𐶐�����
        /// </summary>
        /// <returns>���������L�����N�^�[</returns>
        public Actor Spawn(BehaviorType behavior)
        {
            Actor actor = Instantiate(_behaviorDict[behavior]);
            return actor;
        }
    }
}
