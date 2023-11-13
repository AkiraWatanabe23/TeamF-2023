using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// 注文を待つステート
    /// </summary>
    public class OrderState : BaseState
    {
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] Collider _collider;

        EmptyTable _table;

        public override StateType Type => StateType.Order;
        
        /// <summary>
        /// 結果が確定した(成功/失敗)場合は外部から次のステートに遷移させる
        /// </summary>
        public OrderResult Result { get; private set; }

        protected override void OnAwakeOverride()
        {
            // このステートがStayの際は当たり判定が有効になる
            // Initは初期化される度に呼ばれるのでAwakeのタイミングで1度だけ登録する
            this.OnCollisionEnterAsObservable().Where(_ => CurrentStage == Stage.Stay).Subscribe(OnItemHit);
        }

        public void Init(EmptyTable table)
        {
            _table = table;
            Result = OrderResult.Unsettled;
        }

        protected override void Enter()
        {
            LookAt();
            Animator.Play("Order");
            Cri.PlaySE("SE_ChinBell");

            // 席を有効化、時間切れ(失敗)もしくはキャッチ判定(成功)でコールバックが呼ばれる
            _table.Table.Valid(_settings.OrderTimeLimit, _settings.RandomOrder, result => 
            {
                Result = result;
                _table.Table.Invalid();
            });
        }

        protected override void Exit()
        {
            // 時間内かつ、まだキャッチしていない状態、アイテムをぶつけられていない状態で
            // 外部からのトリガーでステートを抜ける可能性を考慮して無効化する
            _table.Table.Invalid();
        }

        protected override void Stay()
        {
            // このステートは非同期処理で実行されるので、Enterが呼ばれてからはStayを呼ばなくても進む
        }

        /// <summary>
        /// 席に向ける
        /// </summary>
        void LookAt()
        {
            Vector3 dir = _table.Position - transform.position;
            if (dir != Vector3.zero)
            {
                Model.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
        }

        /// <summary>
        /// アイテムがぶつかった場合のコールバック
        /// </summary>
        void OnItemHit(Collision collision)
        {
            // アイテム以外がぶつかった場合は弾く
            if (!collision.gameObject.TryGetComponent(out ThrowedItem _)) return;

            // アイテムがぶつかった場合は席側で判定しないので、こちら側で無効化し、結果を失敗にする
            _table.Table.Invalid();
            Result = OrderResult.Failure;

            // 音とパーティクル
            Cri.PlaySE("SE_OrderHit");
            ParticleType particle = _settings.ItemHitParticle;
            Vector3 position = transform.position + _settings.ItemHitParticleOffset;
            ParticleMessageSender.SendMessage(particle, position, transform);
        }
    }
}