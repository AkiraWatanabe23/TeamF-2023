using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Alpha
{
    /// <summary>
    /// 注文されたアイテムをキャッチする判定のクラス
    /// あらかじめ、判定内のアイテムをチェックするObservableを作成しておき、
    /// 待つ際にアイテムを指定し、キャッチ判定がされるまで待つ。
    /// </summary>
    public class CatchCollision : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;

        IObservable<Collider> _onTriggerStay;
        // Observableの作成はAwake時点で行うのに対して
        // 注文は待機時に決まるのでメンバとして保持しておく必要がある。
        ItemType _order;

        void Awake()
        {
            // 子オブジェクトからコライダーを取得し、
            // 何かが判定に接触している場合は対象をチェックする。
            Collider collider = GetComponentInChildren<Collider>();
            _onTriggerStay = collider.OnTriggerStayAsObservable().Where(c => Check(c));
        }

        /// <summary>
        /// 注文したアイテムかつ、キャッチ可能な速度の場合は、成功とする。
        /// アイテム側のキャッチした際のメソッドを呼び出す。
        /// </summary>
        bool Check(Collider collider)
        {
            if (collider.TryGetComponent(out ICatchable catchable))
            {
                if (catchable.Type == _order && catchable.SqrSpeed <= _settings.CatchSettings.CatchableSpeed)
                {
                    catchable.Catch();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// アイテムを指定して、キャッチするまで待つ
        /// </summary>
        /// <returns>アイテムをキャッチ: 成功</returns>
        public async UniTask<OrderResult> WaitAsync(ItemType order, CancellationToken token)
        {
            _order = order;

            await _onTriggerStay.ToUniTask(useFirstValue: true, cancellationToken: token);

            return OrderResult.Success;
        }
    }
}
