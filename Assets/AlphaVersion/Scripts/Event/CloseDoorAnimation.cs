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
        [SerializeField] Transform _left;
        [SerializeField] Transform _right;
        [SerializeField] Transform _board;
        [SerializeField] float _duration;

        void Awake()
        {
            // ��\���ɂ��Ă���
            _left.localScale = Vector3.zero;
            _right.localScale = Vector3.zero;
            _board.localScale = Vector3.zero;
        }

        /// <summary>
        /// �h�A�̕܂�A�j���[�V�������Đ����A�I���܂ő҂�
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _left.localScale = Vector3.one;
            _right.localScale = Vector3.one;
            _board.localScale = Vector3.one;

            _left.DOLocalRotate(Vector3.zero, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _right.DOLocalRotate(Vector3.zero, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
            
            _board.DOLocalMoveY(-0.25f, _duration).SetEase(Ease.InCubic).SetLink(gameObject);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
        }
    }
}
