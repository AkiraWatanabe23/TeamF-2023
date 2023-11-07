using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 各キャラクターのパラメータの構造体
    /// </summary>
    [System.Serializable]
    public struct ActorParams
    {
        [Header("移動速度")]
        [Range(0, 1.0f)]
        public float MoveSpeed;
        [Header("注文品の一覧")]
        public ItemType[] Orders;
        [Header("注文を受け取る制限時間(秒)")]
        [Range(1.0f, 60.0f)]
        public float OrderTimeLimit;
    }

    /// <summary>
    /// キャラクターの各種値を設定するクラス
    /// </summary>
    [CreateAssetMenu(fileName = "ActorSettingsSO", menuName = "Settings/ActorSettings")]
    public class ActorSettingsSO : ScriptableObject
    {
        [Header("キャラクターを識別する値")]
        [SerializeField] ActorType _actorType;
        [SerializeField] BehaviorType _behaviorType;
        [Header("プランナーが弄る値")]
        [SerializeField] ActorParams _params;

        public float MoveSpeed => _params.MoveSpeed;
        public ItemType[] Orders => _params.Orders;
        public ItemType RandomOrder => Orders[Random.Range(0, Orders.Length)];
        public float OrderTimeLimit => _params.OrderTimeLimit;
        public ActorType ActorType => _actorType;
        public BehaviorType BehaviorType => _behaviorType;
    }
}
