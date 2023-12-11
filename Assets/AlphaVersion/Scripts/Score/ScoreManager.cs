using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// ���݂̃X�R�A���Ǘ�����N���X
    /// ���b�Z�[�W�̎�M�ő������s��
    /// </summary>
    public class ScoreManager : MonoBehaviour, ITotalScoreReader
    {
        [SerializeField] ScoreMessageReceiver _messageReceiver;
        [SerializeField] ScoreUI _scoreUI;
        [SerializeField] ScoreCalculator _calculator;
        [SerializeField] MoneyTextScripts _money;

        ReactiveProperty<int> _totalScore = new(0);

        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore;

        void OnEnable()
        {
            _messageReceiver.OnMessageReceived += OnMessageReceived;
        }

        void OnDisable()
        {
            _messageReceiver.OnMessageReceived -= OnMessageReceived;
        }

        void Start()
        {
            // �X�R�A�̏����l��UI��������
            _scoreUI.Draw(0);
        }

        /// <summary>
        /// Receiver�����b�Z�[�W����M�����ۂ̏���
        /// ���݂̃X�R�A�ɉ��Z����UI�ɔ��f����
        /// </summary>
        void OnMessageReceived(ScoreEventMessage msg)
        {
            int add = _calculator.ToInt(msg);
            _totalScore.Value += add;
            _totalScore.Value = Mathf.Max(_totalScore.Value, 0);
            _scoreUI.Draw(_totalScore.Value);

            _money.MoneyText(add, msg.Position);
        }
    }
}