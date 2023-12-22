using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 客のクラス
    /// </summary>
    public class Customer : Actor
    {
        [SerializeField] AnimationAdapter _adapter;
        [Header("ステート")]
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
            // リストで経路を取得できるように経路の先頭を渡しておく
            _pathConverter = new(lead);
            // 席を取得するのに必要
            _tableManager = arg as TableManager;
            // 移動ステートから開始
            _currentState = _moveState;

            _tension = tension;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // 席の後ろまで移動
            _moveState.Init(_tension, _pathConverter.GetPathToTableBehind());
            while (StepMoveToPathEnd()) await UniTask.Yield(token);
            
            // 席を取得するまで待機
            EmptyTable table;
            while (GetEmptyTable(out table)) await UniTask.Yield(token);
            _adapter.ReservedTable(table.Index);

            // 席まで移動
            _moveState.Init(_tension, _pathConverter.GetPathToTable(table.Waypoint), ignoreForward: true);
            while (StepMoveToPathEnd()) await UniTask.Yield(token);

            // 注文を待つ
            _orderState.Init(table);
            OrderResult result;
            while (StepWaitOrder(out result)) await UniTask.Yield(token);

            // 受け取るのに失敗した場合は注文結果の演出、ラグドールな場合は端折る
            if (result != OrderResult.Defeated)
            {
                _resultState.Init(result);
                while (PlayResultEffect()) await UniTask.Yield(token);
            }

            // 席を解放する
            _tableManager.Release(table);

            // 失敗or成功の場合は帰る、ラグドールな場合は端折る
            if (result != OrderResult.Defeated)
            {
                _moveState.Init(_tension, _pathConverter.GetPathToExit(table.Waypoint), ignoreForward: true);
                while (StepMoveToPathEnd()) await UniTask.Yield(token);
            }

            // TODO:プーリングする
            Destroy(gameObject);
        }

        /// <summary>
        /// 経路の末尾まで移動する
        /// </summary>
        bool StepMoveToPathEnd()
        {
            StepState(_moveState);
            return _moveState.IsRunning;
        }

        /// <summary>
        /// 空席を取得する
        /// </summary>
        bool GetEmptyTable(out EmptyTable table)
        {
            StepState(_idleState);
            // 取得に成功した場合はループを抜ける(false)ので真偽を逆にする
            return !_tableManager.TryGetEmptyRandom(out table);
        }

        /// <summary>
        /// 注文を待つ
        /// </summary>
        bool StepWaitOrder(out OrderResult result)
        {
            StepState(_orderState);
            result = _orderState.Result;
            // 結果が未確定の場合はループする(true)
            return _orderState.Result == OrderResult.Unsettled;
        }

        /// <summary>
        /// 注文が成功/失敗の演出
        /// </summary>
        bool PlayResultEffect()
        {
            StepState(_resultState);
            return _resultState.IsRunning;
        }

        /// <summary>
        /// 1フレーム分だけステートを進める
        /// </summary>
        void StepState(BaseState nextState) => _currentState = _currentState.Step(nextState);
    }
}
