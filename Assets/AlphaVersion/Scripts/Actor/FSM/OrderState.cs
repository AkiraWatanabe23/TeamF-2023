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
        [SerializeField] GameObject _decal;
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] Collider _collider;
        [Header("この速さ以上でぶつかるとラグドール化する")]
        [SerializeField] float _defeatableSpeed = 1.0f;

        EmptyTable _table;

        // 生成したタイミングで初期化処理として初期化クラスから渡す
        public ItemType[] Orders;

        public override StateType Type => StateType.Order;
        
        /// <summary>
        /// 結果が確定した(成功/失敗)場合は外部から次のステートに遷移させる
        /// </summary>
        public OrderResult Result { get; private set; }

        protected override void OnAwakeOverride()
        {
            _decal.SetActive(false);

            // このステートがStayの際は当たり判定が有効になる
            // Initは初期化される度に呼ばれるのでAwakeのタイミングで1度だけ登録する
            _collider.OnCollisionEnterAsObservable().Where(_ => CurrentStage == Stage.Stay).Subscribe(OnItemHit);
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
            Cri.PlaySE3D(transform.position, _settings.OrderVoice, "CueSheet_SE4");

            // 席を有効化、時間切れ(失敗)もしくはキャッチ判定(成功)でコールバックが呼ばれる
            _table.Table.Valid(_settings.OrderTimeLimit, Orders[Random.Range(0, Orders.Length)], result => 
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
            Vector3 p1 = new Vector3(_table.Position.x, 0, _table.Position.z);
            Vector3 p2 = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = p1 - p2;

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
            if (!collision.gameObject.TryGetComponent(out ThrowedItem item)) return;

            _decal.SetActive(true);

            // 注文結果
            if (Result == OrderResult.Unsettled)
            {
                // 速度が一定以上の場合は撃破され、ラグドールを生成する
                if (item.SqrSpeed > _defeatableSpeed)
                {
                    Result = OrderResult.Defeated;
                    RagDollMessageSender.SendMessage(_settings.ActorType, Model, item.transform.position);
                }
                else
                {
                    Result = OrderResult.Failure;
                }

                // アイテムがぶつかった場合は席側で判定しないので、こちら側で無効化し、結果を失敗にする
                _table.Table.Invalid();
            }

            // パーティクル、音はアイテム側が再生
            ParticleType particle = _settings.ItemHitParticle;
            Vector3 position = transform.position + _settings.ItemHitParticleOffset;
            ParticleMessageSender.SendMessage(particle, position, transform);
        }
    }
}