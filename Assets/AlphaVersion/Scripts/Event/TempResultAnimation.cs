using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// 仮のリザルト表示アニメーション
    /// メッセージを表示非表示の切り替えで表示するだけ
    /// </summary>
    public class TempResultAnimation : MonoBehaviour
    {
        [SerializeField] GameObject _message;
        [SerializeField] GameObject _resultMessage;

        void Awake()
        {
            _message.SetActive(false);
            _resultMessage.SetActive(false);
        }

        /// <summary>
        /// 表示の切り替えを行い、一応1フレーム待つだけ
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            _message.SetActive(true);
            _resultMessage.SetActive(true);

            // リザルト文字の反映
            _resultMessage.GetComponent<Text>().text = result.ToString();

            await UniTask.Yield(token);
        }
    }
}
