using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 時間経過による客とギミックの生成を行うクラス
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
        /// インゲーム側から毎フレーム呼ぶことで時間経過により、一定間隔で生成する
        /// </summary>
        public void Tick(float elapsed)
        {
            _customerElapsed += Time.deltaTime;
            _robberElapsed += _settings.Robber.FixedDelta * Time.deltaTime;
            _tumbleweedElapsed += _settings.TumbleWeed.FixedDelta * Time.deltaTime;

            // 客
            if (_customerElapsed > _settings.CustomerSpawnRate)
            {
                _customerElapsed = 0;
                _actorSpawnManager.TrySpawnRandomCustomer();
            }

            // 強盗
            if (_robberTimingIndex < _settings.Robber.Timing.Count &&
                _robberElapsed > _settings.Robber.Timing[_robberTimingIndex])
            {
                _robberTimingIndex++;
                //_robberElapsed = 0;
                _actorSpawnManager.SpawnRobber();
            }

            // タンブル
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