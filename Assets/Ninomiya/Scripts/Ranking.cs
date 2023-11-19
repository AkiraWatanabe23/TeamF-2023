using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("今回のスコアを表示するためのTest")] private Text _currentText;

    [SerializeField, Header("順位スコアを表示するためのTest")] private List<Text> _texts = new List<Text>();

    [SerializeField] private bool _tmpBool = false; //ゲームが終わった際の判定するための仮のBool(使わない

    [SerializeField] private bool _gameEnd = false;//(使わない

    [SerializeField, Header("キャンバス")] private GameObject _scorePanel;

    private RankingSystem _rankingSystem;

    // Start is called before the first frame update
    void Start()
    {
        _rankingSystem = FindObjectOfType<RankingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_tmpBool)
        {
            if (_gameEnd)
            {
                GetTmpScoreEffect();
            }
        }
    }

    public void GetTmpScoreEffect()
    {
        int tmpCurrentScore = 2000;　//今回のスコアを入れてもらう(参照する
        _rankingSystem.AddPlayerScore(tmpCurrentScore);
        var scores = _rankingSystem.GetScores(5);

        _scorePanel.transform.DOLocalMoveX(0, 5f).OnComplete(() =>
        {
            _scorePanel.transform.DOLocalMoveX(0, 01f);
            _currentText.text = $"Score  :  {tmpCurrentScore}";

            _currentText.DOFade(endValue: 1f, duration: 0.5f).OnComplete(() =>
          {
              var sequence = DOTween.Sequence();
              for (int i = 1; i <= scores.Count; i++)
              {
                  sequence.Append(_texts[i - 1].DOFade(endValue: 1, duration: 1));
                  //_texts[i - 1].DOCounter(0, scores[i - 1].Score, 1f);
                  _texts[i - 1].text = $"{i}st : {scores[i - 1].Score}";
              }
              sequence.Play();
          });
        });
    }
}
