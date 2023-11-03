using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ��̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "HandSettingsSO", menuName = "Settings/HandSettings")]
    public class HandSettingsSO : ScriptableObject
    {
        [System.Serializable]
        public struct Offset
        {
            const float Range = 0.2f;

            [Range(-Range, Range)]
            [SerializeField] float x;
            [Range(-Range, Range)]
            [SerializeField] float y;
            [Range(-Range, Range)]
            [SerializeField] float z;
            [Header("�����_����")]
            [Range(0, Range)]
            [SerializeField] float _random;

            public Vector3 Position
            {
                get
                {
                    Vector3 p = new Vector3(x, y, z);
                    //p.y += Random.Range(-_random, _random);
                    p.z += Random.Range(-_random, _random);
                    return p;
                }
            }
        }

        [Header("����u���p�[�e�B�N���̈ʒu�̃I�t�Z�b�g")]
        [SerializeField] Offset _thunParticleOffset;
        [Header("�����Ă����p�[�e�B�N���̈ʒu�̃I�t�Z�b�g")]
        [SerializeField] Offset _swooshParticleOffset;
        [Header("�����Ă����p�[�e�B�N�����A�C�e����Ǐ]����")]
        [SerializeField] bool _swooshParticleItemFollow = true;
        [Header("�O���X�������p�[�e�B�N���̈ʒu�̃I�t�Z�b�g")]
        [SerializeField] Offset _crashParticleOffset;

        public Vector3 StackParticleOffset => _thunParticleOffset.Position;
        public Vector3 ThrowParticleOffset => _swooshParticleOffset.Position;
        public Vector3 CrashParticleOffset => _crashParticleOffset.Position;
        public bool SwooshParticleItemFollow => _swooshParticleItemFollow;
    }
}
