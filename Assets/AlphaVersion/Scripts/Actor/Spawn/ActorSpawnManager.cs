using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���Ԋu�ŃL�����N�^�[�̐������s�����Ǘ�����N���X
    /// </summary>
    public class ActorSpawnManager : MonoBehaviour
    {
        [SerializeField] ActorInitializer _initializer;
        [SerializeField] ActorSpawnTimer _timer;
        [SerializeField] SpawnRangeChecker _checker;
        [SerializeField] GimmickProvider _gimmick; // TODO:�{���Ȃ�C���^�[�t�F�[�X�ŎQ��
        [Header("�f�o�b�O�p: Z�L�[�ŋq/C�L�[�ŋ����𐶐�")]
        [SerializeField] bool _isDebug;

        void OnEnable()
        {
            // �C���Q�[���J�n�Ń^�C�}�[���X�^�[�g����̂ŁA����Q�[���J�n�t���O���`�F�b�N����K�v������
            _timer.OnSpawnTiming += TrySpawnCustomer;
            _gimmick.OnRobberSpawned += SpawnRobber;
        }

        void OnDisable()
        {
            _timer.OnSpawnTiming -= TrySpawnCustomer;
            _gimmick.OnRobberSpawned -= SpawnRobber;
        }

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
        void TrySpawnCustomer()
        {
            // TODO:�L�����N�^�[�̐����͋q�A�����A�M�~�b�N�����邪�A����q�����̏ꍇ�ō���Ă���B
            if (!_isDebug && _checker.Check())
            {
                _initializer.Initialize(BehaviorType.Customer, ActorType.Male);
            }
        }

        /// <summary>
        /// �����̐������s��
        /// </summary>
        void SpawnRobber()
        {
            _initializer.Initialize(BehaviorType.Robber, ActorType.Robber);
        }
    }
}