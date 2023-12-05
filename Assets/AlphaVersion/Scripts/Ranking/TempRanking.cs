using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// ランキングを扱うためのクラス
    /// </summary>
    public class TempRanking : MonoBehaviour
    {
        [SerializeField, Header("今回のスコアを表示するためのTest")] private Text _currentText;

        [SerializeField, Header("順位スコアを表示するためのTest")] private List<Text> _texts = new List<Text>();

        [SerializeField] private bool _tmpBool = false; //ゲームが終わった際の判定するための仮のBool(使わない

        [SerializeField] private bool _gameEnd = false;//(使わない

        [SerializeField, Header("キャンバス")] private GameObject _scorePanel;

        private TempRankingSystem _rankingSystem;

        private List<PlayerScore> _playerScores = new();

        [SerializeField, Header("スコア取得までの待ち時間")] private float _waitTime;

        [SerializeField, Header("取得したいスコアの数")] private int _scoreCount;

        [SerializeField, Header("ボタンを格納")] GameObject[] _buttons = new GameObject[2];

        // Start is called before the first frame update
        void Start()
        {
            _rankingSystem = FindObjectOfType<TempRankingSystem>();
            StartCoroutine(GetScores(100));
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

        public IEnumerator GetScores(int score)
        {
            yield return new WaitForSeconds(_waitTime);

            GetTmpScoreEffect(score);
        }
    }

}