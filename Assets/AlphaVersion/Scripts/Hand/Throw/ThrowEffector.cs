using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������ۂ̉��o���s���N���X
    /// </summary>
    public class ThrowEffector
    {
        HandSettingsSO _settings;

        public ThrowEffector(HandSettingsSO settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// ������Transform�̈ʒu�ŁA�ςލۂ̉��ƃp�[�e�B�N�����Đ�����
        /// </summary>
        public void PlayStackEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_ItemSet");
            Vector3 particlePosition = particleParent.position + _settings.StackParticleOffset;
            ParticleMessageSender.SendMessage(ParticleType.Thun, particlePosition);
        }

        /// <summary>
        /// ������Transform�̈ʒu�ŁA������ۂ̉��ƃp�[�e�B�N�����Đ�����
        /// </summary>
        public void PlayThrowEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_Slide");
            Vector3 particlePosition = particleParent.position + _settings.ThrowParticleOffset;

            // �p�[�e�B�N�����A�C�e����Ǐ]����t���O�������Ă��邩
            Transform parent = _settings.SwooshParticleItemFollow ? particleParent : null;
            ParticleMessageSender.SendMessage(ParticleType.Swoosh, particlePosition, parent);
        }
    }
}