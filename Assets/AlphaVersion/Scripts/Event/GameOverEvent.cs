using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �Q�[���I�[�o�[�̉��o�C�x���g�̃N���X
    /// ���̃C�x���g�I����Ƀ��g���C�ƃV�[���̑J�ڂ��\�ɂȂ�
    /// </summary>
    public class GameOverEvent : MonoBehaviour
    {
        [SerializeField] UIInvisibleAnimation _uiInvisible;
        [SerializeField] CloseDoorAnimation _door;
        [SerializeField] TempResultAnimation _result;

        /// <summary>
        /// �C�x���g�̍Đ�
        /// �A�j���[�V������A�X�R�A�̕\���A����̃L�[���͂�����܂ő҂B
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            await _uiInvisible.PlayAsync(token);
            await _door.PlayAsync(token);
            await _result.PlayAsync(result, token);
        }
    }
}
