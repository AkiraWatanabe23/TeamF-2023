using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// �Q�[���I�[�o�[���̃h�A���܂�A�j���[�V����
    /// (0,0,0)�Ɍ����ĉ�]���ĕ܂�̂ŁALeft��Right�̐e���J�����Ɠ��������������Ă���K�v������B
    /// </summary>
    public class CloseDoorAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] float _duration = 3.0f;

        void Start()
        {
            _animator.transform.localScale = Vector3.zero;
        }

        /// <summary>
        /// �h�A�̕܂�A�j���[�V�������Đ����A�I���܂ő҂�
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _animator.transform.localScale = Vector3.one;
            _animator.enabled = true;

            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
        }
    }
}
