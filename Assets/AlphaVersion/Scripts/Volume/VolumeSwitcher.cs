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
        [Header("�t�B�[�o�[�̃^�C�~���O���g���K�[����")]
        [SerializeField] FerverTrigger _trigger;

        [SerializeField] Volume _volume;
        [SerializeField] Volume _damageVolume;
        [SerializeField] VolumeProfile _normalProfile;
        [SerializeField] VolumeProfile _feverProfile;

        void Awake()
        {
            _damageVolume.enabled = false;
        }

        void OnEnable()
        {
            _trigger.OnFerverEnter += () => Switch(VolumeType.Ferver);
        }

        void OnDisable()
        {
            _trigger.OnFerverEnter -= () => Switch(VolumeType.Ferver);
        }

        /// <summary>
        /// Volume��ύX
        /// </summary>
        public void Switch(VolumeType type)
        {
            if (_damageVolume == null) return;

            if (type == VolumeType.Normal)
            {
                _damageVolume.enabled = false;

            }
            if (type == VolumeType.Ferver)
            {
                _volume.profile = _feverProfile;
                _damageVolume.enabled = false;
            }
            if (type == VolumeType.Damage)
            {
                _damageVolume.enabled = true;
            }
        }
    }
}