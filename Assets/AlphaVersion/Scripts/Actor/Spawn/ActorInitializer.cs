using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���������L�����N�^�[�̏��������s���N���X
    /// Manager��Spawner�̊ԂŁA����������ŋ��n�����s��
    /// </summary>
    public class ActorInitializer : FerverHandler
    {
        [SerializeField] ActorSpawner _spawner;
        [Header("���̃X�e�[�W�Œ����\(Hand���ƍ��킹��)")]
        [SerializeField] ItemType[] _orders;
        [Header("�������ɕK�v�Ȃ���")]
        [SerializeField] PathCreator _pathCreator;
        [SerializeField] TableManager _tableManager;

        /// <summary>
        /// ���������L�����N�^�[�����������ĕԂ�
        /// </summary>
        /// <returns>���������������ς݂̃L�����N�^�[</returns>
        public Actor Initialize(BehaviorType behavior, ActorType actor)
        {
            Actor instance = _spawner.Spawn(behavior, actor);
            Waypoint lead = _pathCreator.GetPath(ToPathType(behavior));

            // �q�̏ꍇ��OrderState�������Ă���̂Ő��������^�C�~���O�łǂ̒����𗊂߂邩��n��
            if (instance.TryGetComponent(out OrderState orderState))
            {
                orderState.Orders = _orders;
            }

            // �q�̏ꍇ�́A�o�H�ƐȂƌ��݃t�B�[�o�[�^�C�����ǂ�����n��
            if (behavior == BehaviorType.Customer) instance.Init(lead, Tension, _tableManager);
            if (behavior == BehaviorType.Robber) instance.Init(lead, Tension);

            return instance;
        }

        /// <summary>
        /// �����̐U�镑���ɑΉ������o�H�̎�ނ�Ԃ�
        /// </summary>
        /// <returns></returns>
        PathType ToPathType(BehaviorType behavior)
        {
            if (behavior == BehaviorType.Customer) return PathType.Customer;
            if (behavior == BehaviorType.Robber) return PathType.Robber;

            throw new System.ArgumentException("�U�镑���ɑΉ������o�H������: " + behavior);
        }
    }
}