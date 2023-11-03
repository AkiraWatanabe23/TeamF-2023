using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���������L�����N�^�[�̏��������s���N���X
    /// Manager��Spawner�̊ԂŁA����������ŋ��n�����s��
    /// </summary>
    public class ActorInitializer : MonoBehaviour
    {
        [SerializeField] ActorSpawner _spawner;
        [Header("�������ɕK�v�Ȃ���")]
        [SerializeField] PathCreator _pathCreator;

        /// <summary>
        /// ���������L�����N�^�[�����������ĕԂ�
        /// </summary>
        /// <returns>���������������ς݂̃L�����N�^�[</returns>
        public Actor Initialize(BehaviorType behavior)
        {
            Actor actor = _spawner.Spawn(behavior);
            Waypoint lead = _pathCreator.GetPath(ToPathType(behavior));
            actor.Init(lead);

            return actor;
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

// ������邱��
// �L�����N�^�[�Ɍo�H��n������
// �����N���X�͐����݂̂��s�����̃N���X�ɕԂ��Ă���
// �o�H�𐶐�����N���X����ǂ�����ēn����
// �o�H��n���C���^�[�t�F�[�X���쐬���ēn����
// object�^��n���C���^�[�t�F�[�X���쐬���ēn����