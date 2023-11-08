using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// ゲームオーバーの演出イベントのクラス
    /// このイベント終了後にリトライとシーンの遷移が可能になる
    /// </summary>
    public class GameOverEvent : MonoBehaviour
    {
        [SerializeField] GameObject _message;
        [SerializeField] GameObject _resultMessage;
        [SerializeField] Transform _shutter;

        void Awake()
        {
            _message.SetActive(false);
            _resultMessage.SetActive(false);
        }

        /// <summary>
        /// イベントの再生
        /// アニメーション後、スコアの表示、特定のキー入力があるまで待つ。
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            await ShutterAnimationAsync(token);

            _message.SetActive(true);
            _resultMessage.SetActive(true);

            // リザルト文字の反映
            _resultMessage.GetComponent<Text>().text = result.ToString();

            // キー入力で次へ進む
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.R));
        }

        /// <summary>
        /// シャッターが降りるアニメーションを再生
        /// </summary>
        async UniTask ShutterAnimationAsync(CancellationToken token)
        {
            Vector3 defaultPos = _shutter.localPosition;
            Vector3 goalPos = Vector3.zero;

            float lerpProgress = 0;
            while (lerpProgress < 1.0f)
            {
                _shutter.localPosition = Vector3.Lerp(defaultPos, goalPos, lerpProgress);
                lerpProgress += Time.deltaTime;

                await UniTask.Yield(token);
            }
        }
    }
}
