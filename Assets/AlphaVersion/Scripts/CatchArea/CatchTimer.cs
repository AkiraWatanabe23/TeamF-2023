using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// キャッチする制限時間を測るクラス
    /// </summary>
    public class CatchTimer : MonoBehaviour
    {
        [SerializeField] Image _circleUI;

        /// <summary>
        /// 制限時間だけ待ち、毎フレームUIを更新する
        /// </summary>
        /// <returns>時間切れ:失敗 キャンセル:失敗</returns>
        public async UniTask<OrderResult> WaitAsync(float timeLimit, CancellationToken token)
        {
            _circleUI.color = Color.white;

            float current = timeLimit;
            while (!token.IsCancellationRequested && current >= 0)
            {
                _circleUI.fillAmount = current / timeLimit;

                current -= Time.deltaTime;
                await UniTask.Yield();
            }

            return OrderResult.Failure;
        }

        public void Invisible()
        {
            _circleUI.color = Color.clear;
        }
    }
}
