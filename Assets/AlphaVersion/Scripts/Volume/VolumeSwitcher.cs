using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Alpha
{
    /// <summary>
    /// ���b�Z�[�W����M�����ۂ�Volume��ύX����@�\�̃N���X
    /// </summary>
    public class VolumeSwitcher : MonoBehaviour
    {
        [SerializeField] Volume _damageVolume;

        void Awake()
        {
            _damageVolume.enabled = false;
        }

        /// <summary>
        /// Volume��ύX
        /// </summary>
        public void Switch(VolumeType type)
        {
            if (type == VolumeType.Normal) _damageVolume.enabled = false;
            if (type == VolumeType.Ferver) _damageVolume.enabled = false;
            if (type == VolumeType.Damage) _damageVolume.enabled = true;
        }
    }
}
