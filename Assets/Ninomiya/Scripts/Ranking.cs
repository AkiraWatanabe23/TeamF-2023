using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("����̃X�R�A��\�����邽�߂�Test")] private Text _currentText;

    [SerializeField, Header("���ʃX�R�A��\�����邽�߂�Test")] private List<Text> _texts = new List<Text>();

    [SerializeField] private bool _tmpBool = false; //�Q�[�����I������ۂ̔��肷�邽�߂̉���Bool(�g��Ȃ�

    [SerializeField] private bool _gameEnd = false;//(�g��Ȃ�

    [SerializeField, Header("�L�����o�X")] private GameObject _scorePanel;

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
        int tmpCurrentScore = 2000;�@//����̃X�R�A�����Ă��炤(�Q�Ƃ���
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
