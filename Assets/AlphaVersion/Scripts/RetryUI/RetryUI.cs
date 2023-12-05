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
    /// ���g���CUI�𑀍삷��N���X
    /// ���o�܂ł�S�����A�J�ڂ͍s��Ȃ�
    /// </summary>
    public class RetryUI : MonoBehaviour
    {
        [SerializeField] Button _retryButton;
        [SerializeField] Button _titleButton;

        /// <summary>
        /// �{�^���N���b�N�܂ő҂��A�N���b�N���ꂽ�{�^���ɉ����Ď��ɑJ�ڂ�����Ԃ�
        /// </summary>
        /// <returns>���̃V�[���������̓^�C�g��</returns>
        public async UniTask<string> ButtonClickAsync(CancellationToken token)
        {
            AsyncUnityEventHandler retry = _retryButton.onClick.GetAsyncEventHandler(token);
            AsyncUnityEventHandler title = _titleButton.onClick.GetAsyncEventHandler(token);

            int result = await UniTask.WhenAny(retry.OnInvokeAsync(), title.OnInvokeAsync());

            // ���o�҂�

            if (result == 0) return SceneManager.GetActiveScene().name;
            else return "Scene_Opningscene";
        }
    }
}
