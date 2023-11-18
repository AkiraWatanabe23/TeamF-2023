using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// ���̃��U���g�\���A�j���[�V����
    /// ���b�Z�[�W��\����\���̐؂�ւ��ŕ\�����邾��
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
        /// �\���̐؂�ւ����s���A�ꉞ1�t���[���҂���
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            _message.SetActive(true);
            _resultMessage.SetActive(true);

            // ���U���g�����̔��f
            _resultMessage.GetComponent<Text>().text = result.ToString();

            await UniTask.Yield(token);
        }
    }
}
