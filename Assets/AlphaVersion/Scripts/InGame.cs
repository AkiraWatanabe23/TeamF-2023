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
    /// �e�@�\��p���ăC���Q�[���̐�����s���N���X
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
        /// �񓯊������̎��s
        /// </summary>
        void Start()
        {
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();

            // �I�u�W�F�N�g�̔j�����Ƀg�[�N����Dispose����
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });
        }

        void OnDestroy()
        {
            CriAudioManager.Instance.SE.StopAll(); // �ꉞ
        }

        /// <summary>
        /// �C���Q�[���̗���
        /// </summary>
        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �Q�[���J�n�̉��o
            await _gameStartEvent.PlayAsync(token);

            // BGM�Đ��A�t�B�[�o�[��BGM�؂�ւ�
            Cri.PlayBGM("BGM_B_Kari");
            _ferver.OnFerverEnter += () => Cri.PlayBGM("BGM_C_DEMO");
            this.OnDisableAsObservable().Subscribe(_ => _ferver.OnFerverEnter -= () => Cri.PlayBGM("BGM_C_DEMO"));

            SendGameStartMessage();

            // ���Ԑ؂�܂Ń��[�v
            float elapsed = 0;
            while (elapsed <= _settings.TimeLimit)
            {
                // �o�ߎ��Ԃ�UI�ɕ\��
                _timerUI.Draw(_settings.TimeLimit, elapsed);

                // �L�����N�^�[�����p�̃^�C�}�[��i�߂�
                _timeSpawn.Tick(elapsed);

                // �t�B�[�o�[�^�C���J�n�܂ł̃^�C�}�[��i�߂�
                _ferver.Tick(elapsed);

                elapsed += Time.deltaTime;
                await UniTask.Yield(token);
            }

            SendGameOverMessage();

            // �Q�[���I���̉��o
            await _gameOverEvent.PlayAsync("����", token);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// �Q�[���J�n�̃��b�Z�[�W���O
        /// </summary>
        void SendGameStartMessage()
        {
            MessageBroker.Default.Publish(new GameStartMessage());
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