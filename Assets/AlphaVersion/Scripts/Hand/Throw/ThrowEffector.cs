using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げる際の演出を行うクラス
    /// </summary>
    public class ThrowEffector
    {
        HandSettingsSO _settings;

        public ThrowEffector(HandSettingsSO settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// 引数のTransformの位置で、積む際の音とパーティクルを再生する
        /// </summary>
        public void PlayStackEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_ItemSet");
            Vector3 particlePosition = particleParent.position + _settings.StackParticleOffset;
            ParticleMessageSender.SendMessage(ParticleType.Thun, particlePosition);
        }

        /// <summary>
        /// 引数のTransformの位置で、投げる際の音とパーティクルを再生する
        /// </summary>
        public void PlayThrowEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_Slide");
            Vector3 particlePosition = particleParent.position + _settings.ThrowParticleOffset;

            // パーティクルがアイテムを追従するフラグが立っているか
            Transform parent = _settings.SwooshParticleItemFollow ? particleParent : null;
            ParticleMessageSender.SendMessage(ParticleType.Swoosh, particlePosition, parent);
        }
    }
}