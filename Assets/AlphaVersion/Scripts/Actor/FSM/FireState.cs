using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Alpha
{
    public struct FireMessage { }

    /// <summary>
    /// ��莞�Ԍo�ߌ�Ɏˌ����s���X�e�[�g
    /// �A�j���[�V�����Đ��̃X�e�[�g��Enter�Ɏˌ��̔񓯊�������ǉ�����
    /// </summary>
    public class FireState : AnimationState
    {
        [Header("�ˌ��̐ݒ�")]
        [SerializeField] float _fireDelay = 0.5f;

        CancellationTokenSource _cts = new();

        public override StateType Type => StateType.Fire;

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        protected override void Enter()
        {
            // �A�j���[�V�����̍Đ�����
            base.Enter();

            // �x�����Ďˌ�
            _cts = new();
            FireAsync(_cts.Token).Forget();
        }

        protected override void Exit()
        {
            // �������Ă��Ȃ����ꉞ
            base.Exit();

            _cts.Cancel();
        }

        /// <summary>
        /// ��莞�Ԍ�Ɏˌ��������b�Z�[�W�𑗐M���邷��
        /// </summary>
        async UniTaskVoid FireAsync(CancellationToken token)
        {
            Cri.PlaySE("SE_Robber_Voice_1");

            await UniTask.Delay(System.TimeSpan.FromSeconds(_fireDelay), cancellationToken: token);

            MessageBroker.Default.Publish(new FireMessage());
            CameraShakeMessageSender.SendMessage(3.0f); // �x�^����
        }
    }
}