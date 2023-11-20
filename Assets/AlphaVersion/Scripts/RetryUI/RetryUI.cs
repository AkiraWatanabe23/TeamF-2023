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
        // TODO:���݂̓��g���C�{�^���̂݁A�^�C�g���ɖ߂�{�^�����{�N�ɍ���Ă��炤

        [SerializeField] Button _button;

        /// <summary>
        /// �{�^���N���b�N�܂ő҂��A�N���b�N���ꂽ�{�^���ɉ����Ď��ɑJ�ڂ�����Ԃ�
        /// </summary>
        /// <returns>���̃V�[���������̓^�C�g��</returns>
        public async UniTask<string> ButtonClickAsync(CancellationToken token)
        {
            AsyncUnityEventHandler handler = _button.onClick.GetAsyncEventHandler(token);
            await handler.OnInvokeAsync();

            // ���o�҂�

            return SceneManager.GetActiveScene().name;
        }
    }
}
