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
        [SerializeField] GimmickSettingsSO _gimmickSettings;
        [SerializeField] SpawnRateSettingsSO _spawnRateSettings;
        [SerializeField] ActorSpawnManager _actorSpawnManager;
        [SerializeField] TumbleweedSpawner _tumbleweedSpawner;
        [SerializeField] UfoSpawner _ufoSpawner;

        float _customerElapsed;
        float _robberElapsed;
        float _tumbleweedElapsed;
        float _ufoElapsed;
        int _robberTimingIndex;
        int _tumbleweedIndex;
        int _ufoIndex;

        /// <summary>
        /// �C���Q�[�������疈�t���[���ĂԂ��ƂŎ��Ԍo�߂ɂ��A���Ԋu�Ő�������
        /// </summary>
        public void Tick(float elapsed)
        {
            _customerElapsed += Time.deltaTime;
            _robberElapsed += Time.deltaTime;
            _tumbleweedElapsed += Time.deltaTime;
            _ufoElapsed += Time.deltaTime;

            // �q
            if (_customerElapsed > _spawnRateSettings.CustomerSpawnRate)
            {
                _customerElapsed = 0;
                _actorSpawnManager.TrySpawnRandomCustomer();
            }

            // ����
            if (_robberTimingIndex < _gimmickSettings.Robber.Max &&
                _robberElapsed > _gimmickSettings.Robber.Timing[_robberTimingIndex])
            {
                Cri.PlaySE("SE_Robber_In");
                _actorSpawnManager.SpawnRobber();
                _robberTimingIndex++;
            }

            // �^���u��
            if (_tumbleweedIndex < _gimmickSettings.Tumbleweed.Max &&
                _tumbleweedElapsed > _gimmickSettings.Tumbleweed.Timing[_tumbleweedIndex].Elapsed)
            {
                int count = _gimmickSettings.Tumbleweed.Timing[_tumbleweedIndex].Count;
                _tumbleweedSpawner.Spawn(count);
                _tumbleweedIndex++;

            }

            // UFO
            if (_ufoIndex < _gimmickSettings.UFO.Max &&
                _ufoElapsed > _gimmickSettings.UFO.Timing[_ufoIndex])
            {
                _ufoSpawner.Spawn();
                _ufoIndex++;
            }
        }
    }
}