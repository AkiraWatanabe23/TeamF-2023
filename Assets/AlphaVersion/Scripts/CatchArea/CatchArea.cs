using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;

namespace Alpha
{
    public enum OrderResult
    {
        Success,
        Failure,
    }

    /// <summary>
    /// 客がアイテムをキャッチするエリアのクラス
    /// </summary>
    public class CatchArea : MonoBehaviour
    {
        [SerializeField] CatchTimer _timer;
        [SerializeField] CatchCollision _collision;
        [SerializeField] CatchTransform _transform;

        CancellationTokenSource _cts = new();
        bool _isFerver; // テスト用のフィーバータイムフラグ
        bool _currentSize;

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        void Update()
        {
            // テスト用
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Valid(ItemType.Alcohol, r => Debug.Log(r));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Invalid();
            }
        }

        /// <summary>
        /// キャッチするアイテムと、コールバックを登録して、有効化する
        /// </summary>
        public void Valid(ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            CatchAsync(order, onCatched).Forget();
        }

        /// <summary>
        /// コールバックを登録削除して無効化する
        /// </summary>
        public void Invalid()
        {
            _cts.Cancel(); // ここまで
        }

        /// <summary>
        /// 時間切れ、キャッチ成功、外部からキャンセル のいずれかまで待つ。
        /// 外部からキャンセルした場合はコールバックが呼ばれないので注意
        /// タイマーの時間切れ: 失敗
        /// コライダーでキャッチ判定: 成功
        /// </summary>
        async UniTaskVoid CatchAsync(ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            // 無効にせず連続で有効にした場合、キャンセルされないのでチェックしておく
            if (!_cts.IsCancellationRequested) _cts.Cancel();
            _cts = new();

            // 時間切れとキャッチ判定のどちらかが完了するまで待つ
            (int win, OrderResult timerResult, OrderResult collisionResult) result;
            result = await UniTask.WhenAny(
                _timer.WaitAsync(2.0f, _cts.Token),
                _collision.WaitAsync(order, _cts.Token));

            if (result.win == 0)
            {
                // 時間切れで 失敗
                onCatched?.Invoke(result.timerResult);
            }
            else
            {
                // キャッチ判定で 成功
                onCatched?.Invoke(result.collisionResult);
            }
        }
    }
}