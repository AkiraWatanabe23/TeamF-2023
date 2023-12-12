using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Alpha
{
    public struct GameStartMessage { }
    public struct GameOverMessage { }

    /// <summary>
    /// 各機能を用いてインゲームの制御を行うクラス
    /// </summary>
    public class InGame : MonoBehaviour
    {
        // ドアが閉まるタイミングと曲の終わりを合わせるためのオフセット
        const float DoorCloseTimingOffset = 8.6f;

        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] GameStartEvent _gameStartEvent;
        [SerializeField] GameOverEvent _gameOverEvent;
        [SerializeField] TimerUI _timerUI;
        [SerializeField] TimeSpawnController _timeSpawn;
        [SerializeField] FerverTrigger _ferver;
        [SerializeField] ScoreManager _score;
        [SerializeField] TempRanking _ranking;
        [SerializeField] RetryUI _retry;

        /// <summary>
        /// 非同期処理の実行
        /// </summary>
        void Start()
        {
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();
            DelayedPlayLastSpurtBGM(cts.Token).Forget();

            // オブジェクトの破棄時にトークンをDisposeする
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });
        }

        void OnDisable()
        {
            Cri.StopAll(); // 一応
        }

        /// <summary>
        /// インゲームの流れ
        /// </summary>
        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            await TryFadeInAsync(token);
            //await _gameStartEvent.PlayAsync(token);

            Cri.PlayBGM("BGM_B_Kari");
            RegisterFerverBGM();
            SendGameStartMessage();

            // 時間切れまでループ
            for (float t = 0; t <= _settings.TimeLimit; t += Time.deltaTime)
            {
                Step(t);
                await UniTask.Yield(token);
            }

            //Cri.StopBGM();
            SendGameOverMessage();


            string evaluate = _settings.GetEvaluate(_score.TotalScore.Value);
            _ranking.GetTmpScoreEffect(_score.TotalScore.Value);
            await _gameOverEvent.PlayAsync(evaluate, token);

            string nextScene = await _retry.ButtonClickAsync(token);
            SceneManager.LoadScene(nextScene);
        }

        /// <summary>
        /// フェードの機能がある場合はフェードインする
        /// </summary>
        async UniTask TryFadeInAsync(CancellationToken token)
        {
            if (Fade.Instance == null) await UniTask.Yield(token);
            else
            {
                bool onCompleted = false;
                Fade.Instance.StartFadeIn(() => onCompleted = true);

                await UniTask.WaitUntil(() => onCompleted, cancellationToken: token);
            }
        }

        /// <summary>
        /// BGMを再生し、フィーバータイム突入でBGM切り替え
        /// </summary>
        void RegisterFerverBGM()
        {
            _ferver.OnFerverEnter += () => Cri.PlayBGM("BGM_C_DEMO");
            this.OnDisableAsObservable().Subscribe(_ => _ferver.OnFerverEnter -= () => Cri.PlayBGM("BGM_C_DEMO"));
        }

        /// <summary>
        /// 〆のBGMを再生
        /// </summary>
        async UniTaskVoid DelayedPlayLastSpurtBGM(CancellationToken token)
        {
            // 制限時間-10秒で残り10秒で再生
            await UniTask.Delay(System.TimeSpan.FromSeconds(_settings.TimeLimit - DoorCloseTimingOffset), cancellationToken: token);
            Cri.PlayBGM("BGM_C'_DEMO");
        }


        /// <summary>
        /// ゲーム開始のメッセージング
        /// </summary>
        void SendGameStartMessage()
        {
            MessageBroker.Default.Publish(new GameStartMessage());
        }

        /// <summary>
        /// 各種機能を1フレーム分進める
        /// </summary>
        void Step(float elapsed)
        {
            // 経過時間をUIに表示
            _timerUI.Draw(_settings.TimeLimit, elapsed);

            // キャラクター生成用のタイマーを進める
            _timeSpawn.Tick(elapsed);

            // フィーバータイム開始までのタイマーを進める
            _ferver.Tick(elapsed);
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