using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �������ς݂̃L�����N�^�[���ꗗ�ŕێ�����N���X
    /// </summary>
    public class ActorSpawnTable : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;

        /// <summary>
        /// ���������L�����N�^�[�����������ĕԂ�
        /// </summary>
        /// <returns>���������������ς݂̃L�����N�^�[</returns>
        public Actor Initialize(BehaviorType behavior, ActorType actor)
        {
            // ���̃N���X�Ńv�[�����Ԃ牺���邱�ƂŃv�[�����O������o���N���X�ɂ���
            // �Ԃ牺�����v�[���̃N���X����e�L�����N�^�[�̏������ς݂����
            // Table->Holder->Init->Create �̏�

            // TODO:�L�����N�^�[�̃v�[�����O�@�\�����
            return _initializer.Initialize(behavior, actor);
        }
    }
}
