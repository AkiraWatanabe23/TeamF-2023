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
    /// ゲームの進行状況に応じてVolumeを変更する機能のクラス
    /// </summary>
    public class VolumeController : FerverHandler
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] VolumeSwitcher _switcher;

        // ダメージから元の状態に戻すため、現在のvolumeを保持する
        VolumeType _currentVolume = VolumeType.Normal;

        protected override void OnAwakeOverride()
        {
            // 攻撃のメッセージを受信することでダメージのVolumeに変更する
            CancellationTokenSource cts = new();
            MessageBroker.Default.Receive<FireMessage>().Subscribe(_ =>
            {
                DamageAsync(cts.Token).Forget();
            }).AddTo(gameObject);

            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });

            // ゲームオーバー時に血糊を消す
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => 
            {
                _switcher.Switch(_currentVolume);
            });
        }

        /// <summary>
        /// フィーバーになったタイミングでフィーバーのVolumeに変更
        /// </summary>
        protected override void OnFerverTimeEnter()
        {
            _currentVolume = VolumeType.Ferver;
        }

        /// <summary>
        /// ダメージのVolumeに変更し、一定時間後、元のVolumeに戻す
        /// </summary>
        async UniTask DamageAsync(CancellationToken token)
        {
            _switcher.Switch(VolumeType.Damage);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_settings.DamagedPenalty), cancellationToken: token);
            _switcher.Switch(_currentVolume);
        }
    }
}
