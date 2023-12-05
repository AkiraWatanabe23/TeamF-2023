using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げる際の演出を行うクラス
    /// </summary>
    [System.Serializable]
    public class ThrowEffector
    {
        [SerializeField] Vector3 _stackParticleOffset;
        [SerializeField] Vector3 _throwParticleOffset;
        [Header("パーティクルがアイテムを追従する")]
        [SerializeField] bool _swooshParticleItemFollow;

        /// <summary>
        /// 引数のTransformの位置で、積む際の音とパーティクルを再生する
        /// </summary>
        public void PlayStackEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_ItemSet");
            Vector3 particlePosition = particleParent.position + _stackParticleOffset;
            ParticleMessageSender.SendMessage(ParticleType.Thun, particlePosition);
        }

        /// <summary>
        /// 引数のTransformの位置で、投げる際の音とパーティクルを再生する
        /// </summary>
        public void PlayThrowEffect(Transform particleParent, Transform pivot)
        {
            Cri.PlaySE("SE_Slide");
            Cri.DelayedPlaySE("SE_Swoosh_KARI", 0.1f, "CueSheet_SE2");
            Vector3 particlePosition = pivot.position + _throwParticleOffset;
            
            Transform parent = _swooshParticleItemFollow ? particleParent : null;
            ParticleMessageSender.SendMessage(ParticleType.Swoosh, particlePosition, parent);
        }
    }
}