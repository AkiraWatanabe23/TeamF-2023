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
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] SpawnRangeChecker _checker;
        [Header("�f�o�b�O�p: Z�L�[�ŋq/C�L�[�ŋ����𐶐�")]
        [SerializeField] bool _isDebug;

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
                    _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
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
                // ��������L�����N�^�[�͏d�ݕt���Œ��I�����
                _initializer.Initialize(BehaviorType.Customer, _settings.RandomCustomerType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// �����̐������s��
        /// </summary>
        public void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
        }
    }
}