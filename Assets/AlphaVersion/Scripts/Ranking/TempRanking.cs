using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// �����L���O���������߂̃N���X
    /// </summary>
    public class TempRanking : MonoBehaviour
    {
        [SerializeField, Header("����̃X�R�A��\�����邽�߂�Test")] private Text _currentText;

        [SerializeField, Header("���ʃX�R�A��\�����邽�߂�Test")] private List<Text> _texts = new List<Text>();

        private TempRankingSystem _rankingSystem;

        private List<PlayerScore> _playerScores = new();

        [SerializeField, Header("�X�R�A�擾�܂ł̑҂�����")] private float _waitTime;

        [SerializeField, Header("�擾�������X�R�A�̐�")] private int _scoreCount;

        [SerializeField, Header("�{�^�����i�[")] GameObject[] _buttons = new GameObject[2];

        [SerializeField, Header("enabled��؂�ւ��邽��")] private bool _activeBool = false;

        [SerializeField, Header("Canvas�����܂�")] private Canvas _canvas;

        // Start is called before the first frame update
        void Start()
        {
            _rankingSystem = FindObjectOfType<TempRankingSystem>();
            ActiveChange();
        }
        public void GetTmpScoreEffect(int score)
        {
            _rankingSystem.AddPlayerScore(score);

            _playerScores = _rankingSystem.GetScores(_scoreCount);

            _currentText.text = $"Score : {score}";

            for (int i = 1; i <= _playerScores.Count; i++)
            {
                _texts[i - 1].text = $"{i} : {_playerScores[i - 1].Score}";
                _texts[i - 1].color = new Color(_texts[i - 1].color.r, _texts[i - 1].color.g, _texts[i - 1].color.b, 1);
            }
            _currentText.color = new Color(_currentText.color.r, _currentText.color.g, _currentText.color.b, 1);

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].SetActive(true);
            }
        }

        public void ActiveChange()
        {
            _activeBool = !_activeBool;
            if (_activeBool) { _canvas.enabled = true; }
            else { _canvas.enabled = false; }
        }

        public IEnumerator GetScores(int score)
        {
            yield return new WaitForSeconds(_waitTime);

            GetTmpScoreEffect(score);
        }
    }

}