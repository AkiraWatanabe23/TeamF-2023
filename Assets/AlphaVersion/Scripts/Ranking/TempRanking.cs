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

        [SerializeField] private bool _tmpBool = false; //�Q�[�����I������ۂ̔��肷�邽�߂̉���Bool(�g��Ȃ�

        [SerializeField] private bool _gameEnd = false;//(�g��Ȃ�

        [SerializeField, Header("�L�����o�X")] private GameObject _scorePanel;

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

                // TODO:Dotween�ăC���X�g�[���Œ���炵���G���[
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

                // �ꎞ�I
                _currentText.color = Color.white;
                _texts.ForEach(t => t.color = Color.white);
            });
        }
    }

}