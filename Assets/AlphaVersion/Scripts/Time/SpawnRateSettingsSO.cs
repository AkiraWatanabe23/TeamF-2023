using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �q�̐����m���Ɋւ���ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnRateSettingsSO", menuName = "Settings/SpawnRateSettings")]
    [System.Serializable]
    public class SpawnRateSettingsSO : ScriptableObject
    {
        [Header("�q�̐����Ԋu(�b)")]
        [SerializeField] float _customerSpawnRate = 3.0f;

        public float CustomerSpawnRate => _customerSpawnRate;
    }
}
