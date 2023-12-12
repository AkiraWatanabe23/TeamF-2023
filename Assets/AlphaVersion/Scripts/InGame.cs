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
    /// �e�@�\��p���ăC���Q�[���̐�����s���N���X
    /// </summary>
    public class InGame : MonoBehaviour
    {
        // �h�A���܂�^�C�~���O�ƋȂ̏I�������킹�邽�߂̃I�t�Z�b�g
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
        /// �񓯊������̎��s
        /// </summary>
        void Start()
        {
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();
            DelayedPlayLastSpurtBGM(cts.Token).Forget();

            // �I�u�W�F�N�g�̔j�����Ƀg�[�N����Dispose����
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });
        }

        void OnDisable()
        {
            Cri.StopAll(); // �ꉞ
        }

        /// <summary>
        /// �C���Q�[���̗���
        /// </summary>
        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            await TryFadeInAsync(token);
            //await _gameStartEvent.PlayAsync(token);

            Cri.PlayBGM("BGM_B_Kari");
            RegisterFerverBGM();
            SendGameStartMessage();

            // ���Ԑ؂�܂Ń��[�v
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
        /// �t�F�[�h�̋@�\������ꍇ�̓t�F�[�h�C������
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
        /// BGM���Đ����A�t�B�[�o�[�^�C���˓���BGM�؂�ւ�
        /// </summary>
        void RegisterFerverBGM()
        {
            _ferver.OnFerverEnter += () => Cri.PlayBGM("BGM_C_DEMO");
            this.OnDisableAsObservable().Subscribe(_ => _ferver.OnFerverEnter -= () => Cri.PlayBGM("BGM_C_DEMO"));
        }

        /// <summary>
        /// �Y��BGM���Đ�
        /// </summary>
        async UniTaskVoid DelayedPlayLastSpurtBGM(CancellationToken token)
        {
            // ��������-10�b�Ŏc��10�b�ōĐ�
            await UniTask.Delay(System.TimeSpan.FromSeconds(_settings.TimeLimit - DoorCloseTimingOffset), cancellationToken: token);
            Cri.PlayBGM("BGM_C'_DEMO");
        }


        /// <summary>
        /// �Q�[���J�n�̃��b�Z�[�W���O
        /// </summary>
        void SendGameStartMessage()
        {
            MessageBroker.Default.Publish(new GameStartMessage());
        }

        /// <summary>
        /// �e��@�\��1�t���[�����i�߂�
        /// </summary>
        void Step(float elapsed)
        {
            // �o�ߎ��Ԃ�UI�ɕ\��
            _timerUI.Draw(_settings.TimeLimit, elapsed);

            // �L�����N�^�[�����p�̃^�C�}�[��i�߂�
            _timeSpawn.Tick(elapsed);

            // �t�B�[�o�[�^�C���J�n�܂ł̃^�C�}�[��i�߂�
            _ferver.Tick(elapsed);
        }

        /// <summary>
        /// �Q�[���I�[�o�[�̃��b�Z�[�W���O
        /// </summary>
        void SendGameOverMessage()
        {
            MessageBroker.Default.Publish(new GameOverMessage());
        }
    }
}