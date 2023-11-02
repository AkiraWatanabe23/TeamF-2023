using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �L���b�`���鐧�����Ԃ𑪂�N���X
    /// </summary>
    public class CatchTimer : MonoBehaviour
    {
        [SerializeField] Image _circleUI;

        /// <summary>
        /// �������Ԃ����҂��A���t���[��UI���X�V����
        /// </summary>
        /// <returns>���Ԑ؂�:���s �L�����Z��:���s</returns>
        public async UniTask<OrderResult> WaitAsync(float timeLimit, CancellationToken token)
        {
            _circleUI.color = Color.white;

            float current = timeLimit;
            while (!token.IsCancellationRequested && current >= 0)
            {
                _circleUI.fillAmount = current / timeLimit;

                current -= Time.deltaTime;
                await UniTask.Yield();
            }

            return OrderResult.Failure;
        }

        public void Invisible()
        {
            _circleUI.color = Color.clear;
        }
    }
}
