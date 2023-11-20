using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("��������")]
        [SerializeField] int _timeLimit = 60;
        [Header("�t�B�[�o�[�^�C���J�n")]
        [SerializeField] int _ferverTime = 20;

        public float TimeLimit => _timeLimit;
        public int FerverTime => _ferverTime;    }
}
