using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �M�~�b�N�Ɋւ���ݒ�
    /// </summary>
    [System.Serializable]
    public class GimmickSettings
    {
        [SerializeField] float _rate = 100.0f;
        [SerializeField] float _delta = 1.0f;
        [Header("�M�~�b�N�����̃����_����")]
        [Range(0, 1.0f)]
        [SerializeField] float _random = 1.0f;

        public float Rate => _rate;
        public float FixedDelta => _delta * Random.Range(1.0f - _random, 1.0f);
    }

    /// <summary>
    /// �C���Q�[���̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("��������")]
        [SerializeField] float _timeLimit;
        [Header("�q�̐����Ԋu")]
        [SerializeField] float _customerSpawnRate = 3.0f;
        [Header("�t�B�[�o�[�ɕK�v�ȃX�R�A")]
        [SerializeField] int _ferverScoreThreshold = 5000;
        [Header("�t�B�[�o�[�̎�������(�b)")]
        [SerializeField] float _ferverTimeLimit = 10.0f;
        [Header("�^���u���E�B�[�h�̃M�~�b�N�̐ݒ�")]
        [SerializeField] GimmickSettings _tumbleweed;
        [Header("�����̃M�~�b�N�̐ݒ�")]
        [SerializeField] GimmickSettings _robber;

        public float TimeLimit => _timeLimit;
        public float CustomerSpawnRate => _customerSpawnRate;
        public int FerverScoreThreshold => _ferverScoreThreshold;
        public float FerverTimeLimit => _ferverTimeLimit;
        public GimmickSettings TumbleWeed => _tumbleweed;
        public GimmickSettings Robber => _robber;
    }
}
