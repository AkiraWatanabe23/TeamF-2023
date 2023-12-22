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
        [SerializeField] AnimationAdapter _adapter;
        [Header("�X�e�[�g")]
        [SerializeField] MoveState _moveState;
        [SerializeField] IdleState _idleState;
        [SerializeField] OrderState _orderState;
        [SerializeField] ResultState _resultState;

        PathConverter _pathConverter;
        TableManager _tableManager;
        BaseState _currentState;
        Tension _tension;

        protected override void OnInitOverride<T>(Waypoint lead, Tension tension, T arg)
        {
            // ���X�g�Ōo�H���擾�ł���悤�Ɍo�H�̐擪��n���Ă���
            _pathConverter = new(lead);
            // �Ȃ��擾����̂ɕK�v
            _tableManager = arg as TableManager;
            // �ړ��X�e�[�g����J�n
            _currentState = _moveState;

            _tension = tension;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �Ȃ̌��܂ňړ�
            _moveState.Init(_tension, _pathConverter.GetPathToTableBehind());
            while (StepMoveToPathEnd()) await UniTask.Yield(token);
            
            // �Ȃ��擾����܂őҋ@
            EmptyTable table;
            while (GetEmptyTable(out table)) await UniTask.Yield(token);
            _adapter.ReservedTable(table.Index);

            // �Ȃ܂ňړ�
            _moveState.Init(_tension, _pathConverter.GetPathToTable(table.Waypoint), ignoreForward: true);
            while (StepMoveToPathEnd()) await UniTask.Yield(token);

            // ������҂�
            _orderState.Init(table);
            OrderResult result;
            while (StepWaitOrder(out result)) await UniTask.Yield(token);

            // �󂯎��̂Ɏ��s�����ꍇ�͒������ʂ̉��o�A���O�h�[���ȏꍇ�͒[�܂�
            if (result != OrderResult.Defeated)
            {
                _resultState.Init(result);
                while (PlayResultEffect()) await UniTask.Yield(token);
            }

            // �Ȃ��������
            _tableManager.Release(table);

            // ���sor�����̏ꍇ�͋A��A���O�h�[���ȏꍇ�͒[�܂�
            if (result != OrderResult.Defeated)
            {
                _moveState.Init(_tension, _pathConverter.GetPathToExit(table.Waypoint), ignoreForward: true);
                while (StepMoveToPathEnd()) await UniTask.Yield(token);
            }

            // TODO:�v�[�����O����
            Destroy(gameObject);
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
