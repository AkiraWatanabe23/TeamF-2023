using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������ۂ̉��o���s���N���X
    /// </summary>
    [System.Serializable]
    public class ThrowEffector
    {
        [SerializeField] Vector3 _stackParticleOffset;
        [SerializeField] Vector3 _throwParticleOffset;
        [Header("�p�[�e�B�N�����A�C�e����Ǐ]����")]
        [SerializeField] bool _swooshParticleItemFollow;

        /// <summary>
        /// ������Transform�̈ʒu�ŁA�ςލۂ̉��ƃp�[�e�B�N�����Đ�����
        /// </summary>
        public void PlayStackEffect(Transform particleParent)
        {
            Cri.PlaySE("SE_ItemSet");
            Vector3 particlePosition = particleParent.position + _stackParticleOffset;
            ParticleMessageSender.SendMessage(ParticleType.Thun, particlePosition);
        }

        /// <summary>
        /// ������Transform�̈ʒu�ŁA������ۂ̉��ƃp�[�e�B�N�����Đ�����
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