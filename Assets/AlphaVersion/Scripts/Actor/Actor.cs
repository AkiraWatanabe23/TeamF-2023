using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    public enum ActorType
    {
        Male,
        Female,
        Muscle,
        Robber,
    }

    public enum BehaviorType
    {
        Customer,
        Robber,
    }

    /// <summary>
    /// キャラクターの基底クラス
    /// 生成時に生成側から呼ばれる Init と Start のオーバーライドが可能
    /// </summary>
    public class Actor : MonoBehaviour
    {
        [SerializeField] ActorSettingsSO _settings;

        public ActorType ActorType => _settings.ActorType;
        public BehaviorType BehaviorType => _settings.BehaviorType;
        protected ActorSettingsSO Settings => _settings;

        /// <summary>
        /// Awakeの代わり、外部から生成する際にこのメソッドを呼ぶ以外に必要な初期化は無い。
        /// </summary>
        public void Init(Waypoint lead, Tension tension)
        {
            OnInitOverride(lead, tension);
        }

        /// <summary>
        /// Awakeの代わり、外部から生成する際にこのメソッドを呼ぶ以外に必要な初期化は無い。
        /// </summary>
        public void Init<T>(Waypoint lead, Tension tension, T arg)
        {
            OnInitOverride(lead, tension, arg);
        }

        /// <summary>
        /// 非同期処理の実行
        /// </summary>
        void Start()
        {
            ExtendCTS cts = new();
            UpdateAsync(cts.Token).Forget();

            // ゲームオーバー時にトークンをDisposeする
            MessageBroker.Default.Receive<GameOverMessage>()
                .Subscribe(_ => cts.Dispose()).AddTo(gameObject);
            // オブジェクトの破棄時にトークンをDisposeする
            this.OnDestroyAsObservable().Subscribe(_ => cts.Dispose());

            OnStartOverride();
        }

        protected virtual void OnInitOverride(Waypoint lead, Tension tension) { }
        protected virtual void OnInitOverride<T>(Waypoint lead, Tension tension, T arg) { }
        protected virtual void OnStartOverride() { }
        protected async virtual UniTaskVoid UpdateAsync(CancellationToken token) { }
    }
}
