using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// インゲーム側の経過時間でキャラクターの生成イベントを起こす機能のクラス
    /// </summary>
    public class ActorSpawnTimer : MonoBehaviour
    {
        public event UnityAction OnSpawnTiming;

        [SerializeField] InGameSettingsSO _settings;

        float _elapsed;

        void OnDestroy()
        {
            OnSpawnTiming = null;
        }

        /// <summary>
        /// 1フレーム分ずつ経過時間を追加していき、一定間隔でコールバックを呼ぶ
        /// </summary>
        public void Tick()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _settings.CustomerSpawnRate)
            {
                _elapsed = 0;

                OnSpawnTiming?.Invoke();
            }
        }
    }
}
