using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Alpha
{
    /// <summary>
    /// メッセージを受信した際にVolumeを変更する機能のクラス
    /// </summary>
    public class VolumeSwitcher : MonoBehaviour
    {
        [SerializeField] Volume _damageVolume;

        void Awake()
        {
            _damageVolume.enabled = false;
        }

        /// <summary>
        /// Volumeを変更
        /// </summary>
        public void Switch(VolumeType type)
        {
            if (type == VolumeType.Normal) _damageVolume.enabled = false;
            if (type == VolumeType.Ferver) _damageVolume.enabled = false;
            if (type == VolumeType.Damage) _damageVolume.enabled = true;
        }
    }
}
