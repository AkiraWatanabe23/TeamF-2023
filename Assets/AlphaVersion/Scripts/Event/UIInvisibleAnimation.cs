using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ゲームオーバー時にゲーム中のUIを非表示にするアニメーション
    /// </summary>
    public class UIInvisibleAnimation : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] float _duration = 0.1f;

        /// <summary>
        /// ドアの閉まるアニメーションを再生し、終わるまで待つ
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            DOTween.To(() => 1, x => _canvasGroup.alpha = x, 0, _duration);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
        }
    }
}
