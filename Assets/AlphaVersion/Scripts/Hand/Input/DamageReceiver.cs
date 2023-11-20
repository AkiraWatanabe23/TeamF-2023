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
    /// ダメージを受けてしばらくは入力が出来ないようにフラグを立てるするクラス
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
            // 既にダメージを受けた状態の場合は弾く
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
