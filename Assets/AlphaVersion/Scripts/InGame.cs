using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    public struct GameStartMessage { }
    public struct GameOverMessage { }

    /// <summary>
    /// 各機能を用いてインゲームの制御を行うクラス
    /// </summary>
    public class InGame : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] GameStartEvent _gameStartEvent;
        [SerializeField] GameOverEvent _gameOverEvent;
        [SerializeField] TimerUI _timerUI;
        [SerializeField] TimeSpawnController _timeSpawn;
        [SerializeField] FerverTrigger _ferver;

        /// <summary>
        /// 非同期処理の実行
        /// </summary>
        void Start()
        {
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();

            // オブジェクトの破棄時にトークンをDisposeする
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });
        }

        void OnDestroy()
        {
            CriAudioManager.Instance.SE.StopAll(); // 一応
        }

        /// <summary>
        /// インゲームの流れ
        /// </summary>
        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // ゲーム開始の演出
            await _gameStartEvent.PlayAsync(token);

            // BGM再生、フィーバーでBGM切り替え
            Cri.PlayBGM("BGM_B_Kari");
            _ferver.OnFerverEnter += () => Cri.PlayBGM("BGM_C_DEMO");
            this.OnDisableAsObservable().Subscribe(_ => _ferver.OnFerverEnter -= () => Cri.PlayBGM("BGM_C_DEMO"));

            SendGameStartMessage();

            // 時間切れまでループ
            float elapsed = 0;
            while (elapsed <= _settings.TimeLimit)
            {
                // 経過時間をUIに表示
                _timerUI.Draw(_settings.TimeLimit, elapsed);

                // キャラクター生成用のタイマーを進める
                _timeSpawn.Tick(elapsed);

                // フィーバータイム開始までのタイマーを進める
                _ferver.Tick(elapsed);

                elapsed += Time.deltaTime;
                await UniTask.Yield(token);
            }

            SendGameOverMessage();

            // ゲーム終了の演出
            await _gameOverEvent.PlayAsync("成績", token);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// ゲーム開始のメッセージング
        /// </summary>
        void SendGameStartMessage()
        {
            MessageBroker.Default.Publish(new GameStartMessage());
        }

        /// <summary>
        /// ゲームオーバーのメッセージング
        /// </summary>
        void SendGameOverMessage()
        {
            MessageBroker.Default.Publish(new GameOverMessage());
        }
    }
}