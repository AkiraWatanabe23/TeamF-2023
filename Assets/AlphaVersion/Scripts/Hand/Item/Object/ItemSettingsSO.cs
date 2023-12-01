using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムの設定
    /// </summary>
    [CreateAssetMenu(fileName = "ItemSettingsSO", menuName = "Settings/ItemSettings")]
    public class ItemSettingsSO : ScriptableObject
    {
        /// <summary>
        /// 各アイテムのパラメータの構造体
        /// </summary>
        [System.Serializable]
        public struct ItemParams
        {
            [Header("ぶつかった際の割れやすさ")]
            [Range(0, 1.0f)]
            public float _hardness;
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

            [Header("割れる際のパーティクルの設定")]
            public ParticleData CrashParticle;
            public ParticleData CrashEffectParticle;
        }

        [Header("アイテムを識別する値")]
        [SerializeField] ItemType _type;
        [SerializeField] float _height = 0.25f;
        [SerializeField] string _crashSEName;
        [SerializeField] string _hitSEName;
        [Header("プランナーが弄る値")]
        [SerializeField] ItemParams _itemParams;
        [Header("デザイナーが弄る値")]
        [SerializeField] VisualParams _visualParams;

        public ItemType Type => _type;
        public float Height => _height;
        public string CrashSEName => _crashSEName;
        public string HitSEName => _hitSEName;
        public float Hardness => _itemParams._hardness;
        public ParticleType CrashParticle => _visualParams.CrashParticle.Particle;
        public ParticleType CrashEffectParticle => _visualParams.CrashEffectParticle.Particle;
        public Vector3 CrashParticleOffset => _visualParams.CrashParticle.Offset.Position;
        public Vector3 CrashEffectParticleOffset => _visualParams.CrashEffectParticle.Offset.Position;
    }
}
