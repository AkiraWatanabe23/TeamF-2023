using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �L���b�`����Ɋւ���ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "CatchSettingsSO", menuName = "Settings/CatchSettings")]
    [System.Serializable]
    public class CatchSettingsSO : ScriptableObject
    {
        [Header("�L���b�`����̑傫���̐ݒ�")]
        [Range(0.1f, 0.8f)]
        [SerializeField] float _normalSize = 0.25f;
        [Range(0.1f, 0.8f)]
        [SerializeField] float _ferverSize = 0.8f;
        [Header("���i���L���b�`�ł��鑬�x")]
        [SerializeField] float _catchableSpeed = 1.0f;

        public float NormalSize => _normalSize;
        public float FerverSize => _ferverSize;
        public float CatchableSpeed => _catchableSpeed;
    }
}
