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
        Unsettled, // ���m��
    }

    /// <summary>
    /// ��(�q���A�C�e�����L���b�`����G���A)�̃N���X
    /// </summary>
    public class Table : MonoBehaviour, ITableControl
    {
        [SerializeField] CatchTimer _timer;
        [SerializeField] CatchCollision _collision;
        [SerializeField] CatchTransform _transform;
        [SerializeField] OrderView _view;

        CancellationTokenSource _cts = new();
        
        void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        /// <summary>
        /// �L���b�`����A�C�e���ƁA�R�[���o�b�N��o�^���āA�L��������
        /// </summary>
        public void Valid(float timeLimit, ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            CatchAsync(timeLimit, order, onCatched).Forget();
            Vector3 setPosition = _transform.SetRandomPosition();
            _view.Active(order, setPosition);
        }

        /// <summary>
        /// �R�[���o�b�N��o�^�폜���Ė���������
        /// </summary>
        public void Invalid()
        {
            _cts.Cancel();
            _view.Inactive();
            _timer.Invisible();
        }

        /// <summary>
        /// ���Ԑ؂�A�L���b�`�����A�O������L�����Z�� �̂����ꂩ�܂ő҂B
        /// �O������L�����Z�������ꍇ�̓R�[���o�b�N���Ă΂�Ȃ��̂Œ���
        /// �^�C�}�[�̎��Ԑ؂�: ���s
        /// �R���C�_�[�ŃL���b�`����: ����
        /// </summary>
        async UniTaskVoid CatchAsync(float timeLimit, ItemType order, UnityAction<OrderResult> onCatched = null)
        {
            // �����ɂ����A���ŗL���ɂ����ꍇ�A�L�����Z������Ȃ��̂Ń`�F�b�N���Ă���
            if (!_cts.IsCancellationRequested) _cts.Cancel();
            _cts = new();

            // ���Ԑ؂�ƃL���b�`����̂ǂ��炩����������܂ő҂�
            (int win, OrderResult timerResult, OrderResult collisionResult) result;
            result = await UniTask.WhenAny(
                _timer.WaitAsync(timeLimit, _cts.Token),
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