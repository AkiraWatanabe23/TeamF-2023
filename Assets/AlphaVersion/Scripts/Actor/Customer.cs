using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �q�̃N���X
    /// </summary>
    public class Customer : Actor
    {
        [Header("�X�e�[�g")]
        [SerializeField] MoveState _moveState;
        [SerializeField] IdleState _idleState;
        [SerializeField] OrderState _orderState;
        [SerializeField] ResultState _resultState;

        PathConverter _pathConverter;
        TableManager _tableManager;
        BaseState _currentState;

        protected override void OnInitOverride(Waypoint lead, TableManager tableManager, Tension tension)
        {
            // ���X�g�Ōo�H���擾�ł���悤�Ɍo�H�̐擪��n���Ă���
            _pathConverter = new(lead);
            // �Ȃ��擾����̂ɕK�v
            _tableManager = tableManager;
            // �ړ��X�e�[�g����J�n
            _currentState = _moveState;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �Ȃ̌��܂ňړ�
            _moveState.Init(_pathConverter.GetPathToTableBehind());
            while (StepMoveToPathEnd()) await UniTask.Yield(token);
            
            // �Ȃ��擾����܂őҋ@
            EmptyTable table;
            while (GetEmptyTable(out table)) await UniTask.Yield(token);

            // �Ȃ܂ňړ�
            _moveState.Init(_pathConverter.GetPathToTable(table.Waypoint));
            while (StepMoveToPathEnd()) await UniTask.Yield(token);

            // ������҂�
            _orderState.Init(table);
            OrderResult result;
            while (StepWaitOrder(out result)) await UniTask.Yield(token);

            // �������ʂ̉��o
            _resultState.Init(result);
            while (PlayResultEffect()) await UniTask.Yield(token);

            // �Ȃ��������
            _tableManager.Release(table);

            // �A��
            _moveState.Init(_pathConverter.GetPathToExit(table.Waypoint), ignoreForward: true);
            while (StepMoveToPathEnd()) await UniTask.Yield(token);
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
        /// ��Ȃ��擾����
        /// </summary>
        bool GetEmptyTable(out EmptyTable table)
        {
            StepState(_idleState);
            // �擾�ɐ��������ꍇ�̓��[�v�𔲂���(false)�̂Ő^�U���t�ɂ���
            return !_tableManager.TryGetEmptyRandom(out table);
        }

        /// <summary>
        /// ������҂�
        /// </summary>
        bool StepWaitOrder(out OrderResult result)
        {
            StepState(_orderState);
            result = _orderState.Result;
            // ���ʂ����m��̏ꍇ�̓��[�v����(true)
            return _orderState.Result == OrderResult.Unsettled;
        }

        /// <summary>
        /// ����������/���s�̉��o
        /// </summary>
        bool PlayResultEffect()
        {
            StepState(_resultState);
            return _resultState.IsRunning;
        }

        /// <summary>
        /// 1�t���[���������X�e�[�g��i�߂�
        /// </summary>
        void StepState(BaseState nextState) => _currentState = _currentState.Step(nextState);
    }
}
