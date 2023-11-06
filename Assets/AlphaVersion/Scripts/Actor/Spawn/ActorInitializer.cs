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

            // �o�H�ƐȁA���݃t�B�[�o�[�^�C�����ǂ�����n��
            instance.Init(lead, _tableManager, Tension);

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