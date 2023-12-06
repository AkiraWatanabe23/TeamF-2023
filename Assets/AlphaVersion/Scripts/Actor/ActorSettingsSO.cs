using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// キャラクターの各種値を設定するクラス
    /// </summary>
    [CreateAssetMenu(fileName = "ActorSettingsSO", menuName = "Settings/ActorSettings")]
    public class ActorSettingsSO : ScriptableObject
    {
        /// <summary>
        /// 各キャラクターのパラメータの構造体
        /// </summary>
        [System.Serializable]
        public struct ActorParams
        {
            [Header("<color=#00FF76>移動速度</color>")]
            [Range(0.1f, 3.0f)]
            public float MoveSpeed;
            [Header("<color=#00FF76>注文を受け取る制限時間(秒)</color>")]
            [Range(1.0f, 60.0f)]
            public float OrderTimeLimit;
            [Header("注文品の一覧")]
            public ItemType[] Orders;
        }

        /// <summary>
        /// 見てくれの設定用の構造体
        /// </summary>
        [System.Serializable]
        public struct VisualParams
        {
            [System.Serializable]
            public struct Offset
            {
                const float Range = 1f;

                [Range(-Range, Range)]
                [SerializeField] float x;
                [Range(-Range, Range)]
                [SerializeField] float y;
                [Range(-Range, Range)]
                [SerializeField] float z;

                public Vector3 Position
                {
                    get
                    {
                        return new Vector3(x, y, z);
                    }
                }
            }

            [System.Serializable]
            public struct ParticleData
            {
                public ParticleType Particle;
                public Offset Offset;
            }

            [Header("ぶつけた際に再生されるパーティクル")]
            public ParticleData ItemHitParticle;
            [Header("注文の成功/失敗時に再生するパーティクル")]
            public ParticleData SuccessParticle;
            public ParticleData FailureParticle;
        }

        [Header("キャラクターを識別する値")]
        [SerializeField] ActorType _actorType;
        [SerializeField] BehaviorType _behaviorType;
        [SerializeField] string _failureVoice;
        [SerializeField] string _successVoice;
        [SerializeField] string _rareSuccessVoice;
        [Header("<color=#00FF76>レベルデザイン用の値</color>")]
        [SerializeField] ActorParams _actorParams;
        [Header("デザイナーが弄る値")]
        [SerializeField] VisualParams _visualParams;

        public float MoveSpeed => _actorParams.MoveSpeed;
        public ItemType[] Orders => _actorParams.Orders;
        public ItemType RandomOrder => Orders[Random.Range(0, Orders.Length)];
        public float OrderTimeLimit => _actorParams.OrderTimeLimit;
        public ActorType ActorType => _actorType;
        public BehaviorType BehaviorType => _behaviorType;
        public ParticleType SuccessParticle => _visualParams.SuccessParticle.Particle;
        public ParticleType FailureParticle => _visualParams.FailureParticle.Particle;
        public ParticleType ItemHitParticle => _visualParams.ItemHitParticle.Particle;
        public Vector3 SuccessParticleOffset => _visualParams.SuccessParticle.Offset.Position;
        public Vector3 FailureParticleOffset => _visualParams.FailureParticle.Offset.Position;
        public Vector3 ItemHitParticleOffset => _visualParams.ItemHitParticle.Offset.Position;
        public string FailureVoice => _failureVoice;
        // 1/10の低確率でレアな成功ボイス
        public string SuccessVoice => Random.value <= 0.1f ? _rareSuccessVoice : _successVoice;
    }
}