using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムの発生/終了を制御するクラス
    /// インゲーム中の時間経過でフィーバーする
    /// </summary>
    public class FerverTrigger : MonoBehaviour
    {
        // TODO:現状フィーバー開始のみ呼ばれるので、フィーバー終了のハンドリングを行っていない
        public event UnityAction OnFerverEnter;

        [SerializeField] InGameSettingsSO _settings;

        // 1度だけコールバックを呼ぶためのフラグ
        bool _isFerver;

        void OnDisable()
        {
            OnFerverEnter = null;
        }

        /// <summary>
        /// インゲーム側から毎フレーム呼ぶことで時間経過により、フィーバータイム開始
        /// </summary>
        public void Tick(float elapsed)
        {
            // 既にフィーバータイムだった場合は弾く
            if (_isFerver) return;

            if (elapsed > _settings.TimeLimit - _settings.FerverTime)
            {
                OnFerverEnter?.Invoke();
                _isFerver = true;
            }
        }
    }
}
