using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���Ԍo�߂ɂ��q�ƃM�~�b�N�̐������s���N���X
    /// </summary>
    public class TimeSpawnController : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] ActorSpawnManager _actorSpawnManager;
        [SerializeField] TumbleweedSpawner _tumbleweedSpawner;

        float _customerElapsed;
        float _robberElapsed;
        float _tumbleweedElapsed;
        int _robberTimingIndex;
        int _tumbleweedIndex;

        /// <summary>
        /// �C���Q�[�������疈�t���[���ĂԂ��ƂŎ��Ԍo�߂ɂ��A���Ԋu�Ő�������
        /// </summary>
        public void Tick(float elapsed)
        {
            _customerElapsed += Time.deltaTime;
            _robberElapsed += _settings.Robber.FixedDelta * Time.deltaTime;
            _tumbleweedElapsed += _settings.TumbleWeed.FixedDelta * Time.deltaTime;

            // �q
            if (_customerElapsed > _settings.CustomerSpawnRate)
            {
                _customerElapsed = 0;
                _actorSpawnManager.TrySpawnRandomCustomer();
            }

            // ����
            if (_robberTimingIndex < _settings.Robber.Timing.Count &&
                _robberElapsed > _settings.Robber.Timing[_robberTimingIndex])
            {
                _robberTimingIndex++;
                //_robberElapsed = 0;
                _actorSpawnManager.SpawnRobber();
            }

            // �^���u��
            if (_tumbleweedIndex < _settings.TumbleWeed.Timing.Count &&
                _tumbleweedElapsed > _settings.TumbleWeed.Timing[_tumbleweedIndex])
            {
                _tumbleweedIndex++;
                //_tumbleweedElapsed = 0;
                _tumbleweedSpawner.Spawn();
            }
        }
    }
}