using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;

namespace Alpha
{
    public enum OrderResult
    {
        Success,
        Failure,
    }

    /// <summary>
    /// �q���A�C�e�����L���b�`����G���A�̃N���X
    /// </summary>
    public class CatchArea : MonoBehaviour
    {
        [SerializeField] CatchTimer _timer;
        [SerializeField] CatchCollision _collision;
        [SerializeField] CatchTransform _transform;

        CancellationTokenSource _cts = new();
        bool _isFerver; // �e�X�g�p�̃t�B�[�o�[�^�C���t���O
        bool _currentSize;

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        void Update()
        {
            // �e�X�g�p
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Valid(ItemType.Alcohol, r => Debug.Log(r));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Invalid();
            }
        }

        /// <summary>
        /// �L���b�`����A�C�e���ƁA�R�[���o�b�N��o�^���āA�L��������
        /// </summary>
        public void Valid(ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            CatchAsync(order, onCatched).Forget();
        }

        /// <summary>
        /// �R�[���o�b�N��o�^�폜���Ė���������
        /// </summary>
        public void Invalid()
        {
            _cts.Cancel(); // �����܂�
        }

        /// <summary>
        /// ���Ԑ؂�A�L���b�`�����A�O������L�����Z�� �̂����ꂩ�܂ő҂B
        /// �O������L�����Z�������ꍇ�̓R�[���o�b�N���Ă΂�Ȃ��̂Œ���
        /// �^�C�}�[�̎��Ԑ؂�: ���s
        /// �R���C�_�[�ŃL���b�`����: ����
        /// </summary>
        async UniTaskVoid CatchAsync(ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            // �����ɂ����A���ŗL���ɂ����ꍇ�A�L�����Z������Ȃ��̂Ń`�F�b�N���Ă���
            if (!_cts.IsCancellationRequested) _cts.Cancel();
            _cts = new();

            // ���Ԑ؂�ƃL���b�`����̂ǂ��炩����������܂ő҂�
            (int win, OrderResult timerResult, OrderResult collisionResult) result;
            result = await UniTask.WhenAny(
                _timer.WaitAsync(2.0f, _cts.Token),
                _collision.WaitAsync(order, _cts.Token));

            if (result.win == 0)
            {
                // ���Ԑ؂�� ���s
                onCatched?.Invoke(result.timerResult);
            }
            else
            {
                // �L���b�`����� ����
                onCatched?.Invoke(result.collisionResult);
            }
        }
    }
}