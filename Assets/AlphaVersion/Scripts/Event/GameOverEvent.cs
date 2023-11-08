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
        [SerializeField] GameObject _message;
        [SerializeField] GameObject _resultMessage;
        [SerializeField] Transform _shutter;

        void Awake()
        {
            _message.SetActive(false);
            _resultMessage.SetActive(false);
        }

        /// <summary>
        /// �C�x���g�̍Đ�
        /// �A�j���[�V������A�X�R�A�̕\���A����̃L�[���͂�����܂ő҂B
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            await ShutterAnimationAsync(token);

            _message.SetActive(true);
            _resultMessage.SetActive(true);

            // ���U���g�����̔��f
            _resultMessage.GetComponent<Text>().text = result.ToString();

            // �L�[���͂Ŏ��֐i��
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.R));
        }

        /// <summary>
        /// �V���b�^�[���~���A�j���[�V�������Đ�
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
