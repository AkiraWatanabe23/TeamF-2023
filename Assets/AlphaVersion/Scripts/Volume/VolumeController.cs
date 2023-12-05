using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx.Triggers;

namespace Alpha
{
    public enum VolumeType
    {
        Normal,
        Ferver,
        Damage,
    }

    /// <summary>
    /// �Q�[���̐i�s�󋵂ɉ�����Volume��ύX����@�\�̃N���X
    /// </summary>
    public class VolumeController : FerverHandler
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] VolumeSwitcher _switcher;

        // �_���[�W���猳�̏�Ԃɖ߂����߁A���݂�volume��ێ�����
        VolumeType _currentVolume = VolumeType.Normal;

        protected override void OnAwakeOverride()
        {
            // �U���̃��b�Z�[�W����M���邱�ƂŃ_���[�W��Volume�ɕύX����
            CancellationTokenSource cts = new();
            MessageBroker.Default.Receive<FireMessage>().Subscribe(_ =>
            {
                DamageAsync(cts.Token).Forget();
            }).AddTo(gameObject);

            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });

            // �Q�[���I�[�o�[���Ɍ��Ђ�����
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => 
            {
                _switcher.Switch(_currentVolume);
            });
        }

        /// <summary>
        /// �t�B�[�o�[�ɂȂ����^�C�~���O�Ńt�B�[�o�[��Volume�ɕύX
        /// </summary>
        protected override void OnFerverTimeEnter()
        {
            _currentVolume = VolumeType.Ferver;
        }

        /// <summary>
        /// �_���[�W��Volume�ɕύX���A��莞�Ԍ�A����Volume�ɖ߂�
        /// </summary>
        async UniTask DamageAsync(CancellationToken token)
        {
            _switcher.Switch(VolumeType.Damage);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_settings.DamagedPenalty), cancellationToken: token);
            _switcher.Switch(_currentVolume);
        }
    }
}
