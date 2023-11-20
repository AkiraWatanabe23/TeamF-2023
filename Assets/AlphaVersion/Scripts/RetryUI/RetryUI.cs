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
        // TODO:現在はリトライボタンのみ、タイトルに戻るボタンを二宮君に作ってもらう

        [SerializeField] Button _button;

        /// <summary>
        /// ボタンクリックまで待ち、クリックされたボタンに応じて次に遷移する先を返す
        /// </summary>
        /// <returns>このシーンもしくはタイトル</returns>
        public async UniTask<string> ButtonClickAsync(CancellationToken token)
        {
            AsyncUnityEventHandler handler = _button.onClick.GetAsyncEventHandler(token);
            await handler.OnInvokeAsync();

            // 演出待つ

            return SceneManager.GetActiveScene().name;
        }
    }
}
