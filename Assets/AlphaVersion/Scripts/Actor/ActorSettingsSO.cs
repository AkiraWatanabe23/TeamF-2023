using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �e�L�����N�^�[�̃p�����[�^�̍\����
    /// </summary>
    [System.Serializable]
    public struct ActorParams
    {
        [Header("�ړ����x")]
        [Range(0, 1.0f)]
        public float MoveSpeed;
        [Header("�����i�̈ꗗ")]
        public ItemType[] Orders;
        [Header("�������󂯎�鐧������(�b)")]
        [Range(1.0f, 60.0f)]
        public float OrderTimeLimit;
    }

    /// <summary>
    /// �L�����N�^�[�̊e��l��ݒ肷��N���X
    /// </summary>
    [CreateAssetMenu(fileName = "ActorSettingsSO", menuName = "Settings/ActorSettings")]
    public class ActorSettingsSO : ScriptableObject
    {
        [Header("�L�����N�^�[�����ʂ���l")]
        [SerializeField] ActorType _actorType;
        [SerializeField] BehaviorType _behaviorType;
        [Header("�v�����i�[���M��l")]
        [SerializeField] ActorParams _params;

        public float MoveSpeed => _params.MoveSpeed;
        public ItemType[] Orders => _params.Orders;
        public ItemType RandomOrder => Orders[Random.Range(0, Orders.Length)];
        public float OrderTimeLimit => _params.OrderTimeLimit;
        public ActorType ActorType => _actorType;
        public BehaviorType BehaviorType => _behaviorType;
    }
}
