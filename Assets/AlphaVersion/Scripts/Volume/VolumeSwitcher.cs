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
        [SerializeField] VolumeProfile _normal;
        [SerializeField] VolumeProfile _ferver;
        [SerializeField] VolumeProfile _damage;
        [SerializeField] Volume _volume;

        /// <summary>
        /// Volume��ύX
        /// </summary>
        public void Switch(VolumeType type)
        {
            if (type == VolumeType.Normal) _volume.profile = _normal;
            if (type == VolumeType.Ferver) _volume.profile = _ferver;
            if (type == VolumeType.Damage) _volume.profile = _damage;
        }
    }
}
