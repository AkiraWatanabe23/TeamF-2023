using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] ActorSpawnTimer _actorSpawnTimer;

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

        /// <summary>
        /// �C���Q�[���̗���
        /// </summary>
        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �Q�[���J�n�̉��o
            await _gameStartEvent.PlayAsync(token);
            
            SendGameStartMessage();

            // ���Ԑ؂�܂Ń��[�v
            float elapsed = 0;
            while (elapsed <= _settings.TimeLimit)
            {
                // �o�ߎ��Ԃ�UI�ɕ\��
                _timerUI.Draw(_settings.TimeLimit, elapsed);

                // �L�����N�^�[�����p�̃^�C�}�[��i�߂�
                _actorSpawnTimer.Tick();

                elapsed += Time.deltaTime;
                await UniTask.Yield(token);
            }

            SendGameOverMessage();

            // �Q�[���I���̉��o
            await _gameOverEvent.PlayAsync("����", token);
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