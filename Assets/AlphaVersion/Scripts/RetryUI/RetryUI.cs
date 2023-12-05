using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Alpha
{
    /// <summary>
    /// リトライUIを操作するクラス
    /// 演出までを担当し、遷移は行わない
    /// </summary>
    public class RetryUI : MonoBehaviour
    {
        [SerializeField] Button _retryButton;
        [SerializeField] Button _titleButton;

        /// <summary>
        /// ボタンクリックまで待ち、クリックされたボタンに応じて次に遷移する先を返す
        /// </summary>
        /// <returns>このシーンもしくはタイトル</returns>
        public async UniTask<string> ButtonClickAsync(CancellationToken token)
        {
            AsyncUnityEventHandler retry = _retryButton.onClick.GetAsyncEventHandler(token);
            AsyncUnityEventHandler title = _titleButton.onClick.GetAsyncEventHandler(token);

            int result = await UniTask.WhenAny(retry.OnInvokeAsync(), title.OnInvokeAsync());

            // 演出待つ

            if (result == 0) return SceneManager.GetActiveScene().name;
            else return "Scene_Opningscene";
        }
    }
}
