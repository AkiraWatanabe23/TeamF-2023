using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// ギミックの発生を制御するクラス
    /// ギミックの条件はこのクラスに依存する
    /// </summary>
    public class GimmickProvider : ValidStateHandler, IRobberSpawnRegisterable
    {
        public event UnityAction OnTumbleweedSpawned;
        public event UnityAction OnRobberSpawned;

        [SerializeField] InGameSettingsSO _settings;

        float _tumbleweedElapsed;
        float _robberElapsed;

        protected override void OnDisableOverride()
        {
            OnTumbleweedSpawned = null;
            OnRobberSpawned = null;
        }

        /// <summary>
        /// インゲーム開始と同時にUpdateで、ダンブルウィードと強盗
        /// それぞれのタイマーを進め、一定周期で生成処理を呼び出す
        /// </summary>
        protected override void OnUpdateOverride()
        {
            // ダンブルウィード
            _tumbleweedElapsed += _settings.TumbleWeed.FixedDelta;
            if (_tumbleweedElapsed > _settings.TumbleWeed.Rate)
            {
                _tumbleweedElapsed = 0;
                OnTumbleweedSpawned?.Invoke();
            }

            // 強盗
            _robberElapsed += _settings.Robber.FixedDelta;
            if (_robberElapsed > _settings.Robber.Rate)
            {
                _robberElapsed = 0;
                OnRobberSpawned?.Invoke();
            }
        }
    }
}