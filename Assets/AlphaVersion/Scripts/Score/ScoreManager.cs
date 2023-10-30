using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ���݂̃X�R�A���Ǘ�����N���X
    /// ���b�Z�[�W�̎�M�ő������s��
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
            // �X�R�A�̏����l��UI��������
            _scoreUI.Draw(0);
        }

        /// <summary>
        /// Receiver�����b�Z�[�W����M�����ۂ̏���
        /// ���݂̃X�R�A�ɉ��Z����UI�ɔ��f����
        /// </summary>
        void OnMessageReceived(ScoreEventMessage msg)
        {
            _totalScore += _calculator.ToInt(msg);
            _scoreUI.Draw(_totalScore);
        }
    }
}