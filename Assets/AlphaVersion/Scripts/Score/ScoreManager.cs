using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 現在のスコアを管理するクラス
    /// メッセージの受信で増減を行う
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] ScoreMessageReceiver _messageReceiver;
        [SerializeField] ScoreUI _scoreUI;
        
        ScoreCalculator _calculator = new();
        int _totalScore;

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
            _totalScore += _calculator.ToInt(msg);
            _scoreUI.Draw(_totalScore);
        }
    }
}