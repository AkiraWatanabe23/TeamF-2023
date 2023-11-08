using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// �����̃N���X
    /// </summary>
    public class Robber : Actor
    {
        [SerializeField] Collider _collider;
        [Header("�X�e�[�g")]
        [SerializeField] MoveState _moveState;
        [SerializeField] ActionState _actionState;

        PathConverter _pathConverter;
        BaseState _currentState;
        Waypoint _lead; // ����H
        Tension _tension;

        protected override void OnInitOverride(Waypoint lead, Tension tension)
        {
            // ���X�g�Ōo�H���擾�ł���悤�Ɍo�H�̐擪��n���Ă���
            _pathConverter = new(lead);
            // �ړ��X�e�[�g����J�n
            _currentState = _moveState;

            _lead = lead;
            _tension = tension;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �A�C�e�����Ԃ������t���O
            bool isItemHit = false;
            _collider.OnCollisionStayAsObservable().Subscribe(_ => isItemHit = true);

            Waypoint pathEnd = _lead;
            IReadOnlyList<Vector3> path;
            
            // 3�̉��o�n�_
            for (int i = 0; i < 3; i++)
            {
                // ���o�n�_�܂ł̌o�H���擾
                pathEnd = _pathConverter.GetPathByType(WaypointType.Stage, out path, pathEnd);

                // ���o�n�_�܂ňړ�
                _moveState.Init(path);
                while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

                // ���o�n�_�ł̃A�N�V����
                _actionState.Init();
                while (!isItemHit && StepAction()) await UniTask.Yield(token);
            }

            // �ˌ��n�_�܂ł̌o�H���擾
            pathEnd = _pathConverter.GetPathByType(WaypointType.Fire, out path, pathEnd);

            // �ˌ��n�_�܂ňړ�
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // �ˌ�
            _actionState.Init();
            while (!isItemHit && StepAction()) await UniTask.Yield(token);

            // �o���܂ł̌o�H���擾
            path = _pathConverter.GetPathToExit(pathEnd);
            
            // �o���܂ňړ�
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // ���̎��_�ŃA�C�e���ɂԂ����Ă��Ȃ���Ώo�����B���Ă���
            if (!isItemHit)
            {
                // �폜
            }
            else
            {
                // �Ȃ񂩃A�N�V����
                _actionState.Init();
                while (StepAction()) await UniTask.Yield(token);

                // �A��
                path = _pathConverter.GetPathToLeadFromPosition(transform.position);
                _moveState.Init(path, ignoreForward: true);
                while (StepMoveToPathEnd()) await UniTask.Yield(token);
            }
        }

        /// <summary>
        /// �o�H�̖����܂ňړ�����
        /// </summary>
        bool StepMoveToPathEnd()
        {
            StepState(_moveState);
            return _moveState.IsRunning;
        }

        /// <summary>
        /// �A�N�V���������s����
        /// </summary>
        bool StepAction()
        {
            StepState(_actionState);
            return _actionState.IsRunning;
        }

        /// <summary>
        /// 1�t���[���������X�e�[�g��i�߂�
        /// </summary>
        void StepState(BaseState nextState) => _currentState = _currentState.Step(nextState);
    }
}

// ������ƌo�H�����ǂ点��
// �_���u���E�B�[�h