using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// 現在のスコアを管理するクラス
    /// メッセージの受信で増減を行う
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
            // スコアの初期値でUIを初期化
            _scoreUI.Draw(0);
        }

        /// <summary>
        /// Receiverがメッセージを受信した際の処理
        /// 現在のスコアに加算してUIに反映する
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