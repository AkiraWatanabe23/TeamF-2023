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

        // Start is called before the first frame update
        void Start()
        {
            _rankingSystem = FindObjectOfType<TempRankingSystem>();
        }

        public void GetTmpScoreEffect(int currentScore)
        {
            _rankingSystem.AddPlayerScore(currentScore);
            var scores = _rankingSystem.GetScores(5);

            _scorePanel.transform.DOLocalMoveX(0, 1f).OnComplete(() =>
            {
                _scorePanel.transform.DOLocalMoveX(0, 01f);
                _currentText.text = $"Score  :  {currentScore}";

                // TODO:Dotween再インストールで直るらしいエラー
                //_currentText.DOFade(endValue: 1f, duration: 0.5f).OnComplete(() =>
                //{
                //    var sequence = DOTween.Sequence();
                //    for (int i = 1; i <= scores.Count; i++)
                //    {
                //        sequence.Append(_texts[i - 1].DOFade(endValue: 1, duration: 1));
                //        //_texts[i - 1].DOCounter(0, scores[i - 1].Score, 1f);
                //        _texts[i - 1].text = $"{i}st : {scores[i - 1].Score}";
                //    }
                //    sequence.Play();
                //});

                // 一時的
                _currentText.color = Color.white;
                _texts.ForEach(t => t.color = Color.white);
            });
        }
    }

}