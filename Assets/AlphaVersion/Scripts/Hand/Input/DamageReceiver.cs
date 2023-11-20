using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �_���[�W���󂯂Ă��΂炭�͓��͂��o���Ȃ��悤�Ƀt���O�𗧂Ă邷��N���X
    /// </summary>
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;

        CancellationTokenSource _cts = new();

        public event UnityAction OnDamaged;
        public bool IsDamaged { get; private set; }

        void Awake()
        {
            MessageBroker.Default.Receive<FireMessage>()
                .Subscribe(_ => OnMessageReceived()).AddTo(gameObject);
        }

        void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        void OnMessageReceived()
        {
            // ���Ƀ_���[�W���󂯂���Ԃ̏ꍇ�͒e��
            if (IsDamaged) return;
            DamageAsync(_cts.Token).Forget();
            OnDamaged?.Invoke();
        }

        async UniTaskVoid DamageAsync(CancellationToken token)
        {
            IsDamaged = true;
            await UniTask.Delay(System.TimeSpan.FromSeconds(_settings.DamagedPenalty), cancellationToken: token);
            IsDamaged = false;
        }
    }
}
