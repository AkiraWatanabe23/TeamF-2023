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
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();

            // オブジェクトの破棄時にトークンをDisposeする
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });

            OnStartOverride();
        }

        protected virtual void OnInitOverride(Waypoint lead, Tension tension) { }
        protected virtual void OnInitOverride<T>(Waypoint lead, Tension tension, T arg) { }
        protected virtual void OnStartOverride() { }
        protected async virtual UniTaskVoid UpdateAsync(CancellationToken token) { }
    }
}

// 客
//  席まで歩いてくる
//  注文がキャッチできるまで待機
//  アニメーション
//  帰る
// 強盗
//  カウンター隣まで移動
//  構える
//  歩いてくる
//  発砲
//  帰る
// 確定事項
//  頂点にいる状態 と 頂点から頂点に移動する状態 が交互になる
//  フィーバータイムは、アニメーションが切り替わるだけ、現在の行動をキャンセルして何かする訳ではない

// 現在のウェイポイントと経路を保持するクラス
//  現在と離接するウェイポイントに移動可能
//  移動中にキャンセル可能

// キャラ毎に分けるのではなく、頂点の種類ごとに分けるアプローチ？
// キャンセレーショントークンのように各種フラグが参照型で渡されるアプローチ？