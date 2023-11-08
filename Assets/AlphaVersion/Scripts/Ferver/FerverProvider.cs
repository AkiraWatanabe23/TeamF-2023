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
    /// フィーバータイムの発生/終了条件はこのクラスに依存する
    /// </summary>
    public class FerverProvider : MonoBehaviour
    {
        public static event UnityAction<bool> OnFerverSwitched;

        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] ScoreManager _scoreManager; // TODO:本来ならインターフェースで参照する

        CancellationTokenSource _cts;
        // フィーバータイム中にフィーバー開始の閾値に到達した場合、時間を延長する
        float _timeLimit;
        int _nextThreshold;

        void OnDisable()
        {
            OnFerverSwitched = null;
        }

        void Start()
        {
            // 合計スコアが閾値を越えていたらフィーバータイム開始
            _scoreManager.TotalScore.Skip(1).Where(Check).Subscribe(_ => Ferver()).AddTo(gameObject);
            // フィーバーに必要なスコアを設定
            _nextThreshold = _settings.FerverScoreThreshold;
        }

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// 現在の合計スコアが閾値を超えているかを調べる
        /// 超えていた場合は閾値を更新
        /// </summary>
        bool Check(int totalScore)
        {
            if (totalScore >= _nextThreshold)
            {
                _nextThreshold += _settings.FerverScoreThreshold;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通常時ならフィーバータイム開始
        /// フィーバータイム中の場合は時間を延長する
        /// </summary>
        void Ferver()
        {
            if (_timeLimit > 0)
            {
                _timeLimit += _settings.FerverTimeLimit;
            }
            else
            {
                _timeLimit = _settings.FerverTimeLimit;
                _cts = new();
                FerverAsync(_cts.Token).Forget();
            }
        }

        /// <summary>
        /// 制限時間いっぱいフィーバータイムを実行する
        /// </summary>
        async UniTaskVoid FerverAsync(CancellationToken token)
        {
            OnFerverSwitched?.Invoke(true);
            while (_timeLimit >= 0)
            {
                await UniTask.Yield(token);
                _timeLimit -= Time.deltaTime;
            }
            OnFerverSwitched?.Invoke(false);
        }
    }
}
