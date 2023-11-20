using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �L�����N�^�[�̐������s�����Ǘ�����N���X
    /// </summary>
    public class ActorSpawnManager : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] SpawnRangeChecker _checker;
        [Header("�f�o�b�O�p: Z�L�[�ŋq/C�L�[�ŋ����𐶐�")]
        [SerializeField] bool _isDebug;

        /// <summary>
        /// �j�������𓯂��m���̃����_��
        /// </summary>
        ActorType RandomCustomer => Random.value <= 0.5f ? ActorType.Male : ActorType.Female;

        void Update()
        {
            if (_isDebug)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _initializer.Initialize(BehaviorType.Customer, ActorType.Male);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    _initializer.Initialize(BehaviorType.Robber, ActorType.Muscle);
                }
            }
        }

        /// <summary>
        /// �����͈͂Ɋ��ɃL�����N�^�[������ꍇ�͐������s��Ȃ�
        /// ���̏ꍇ�A���̐����^�C�~���O�܂ő҂B
        /// </summary>
        public bool TrySpawnRandomCustomer()
        {
            if (!_isDebug && _checker.Check())
            {
                // TODO:����j�����A���̓��f�����o���Ă���
                _initializer.Initialize(BehaviorType.Customer, ActorType.Male);
                return true;
            }

            return false;
        }

        /// <summary>
        /// �����̐������s��
        /// </summary>
        public void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Muscle);
        }
    }
}