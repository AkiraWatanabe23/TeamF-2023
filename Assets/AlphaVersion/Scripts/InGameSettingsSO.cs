using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̐ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        // �X�R�A�ɂ��]���̐ݒ�
        [System.Serializable]
        public class ScoreEvaluate
        {
            public int Score;
            public string Evaluate;
        }

        [Header("<color=#00FF76>��������</color>")]
        [SerializeField] int _timeLimit = 60;
        [Header("<color=#00FF76>�t�B�[�o�[�^�C���J�n</color>")]
        [SerializeField] int _ferverTime = 20;
        [Header("<color=#00FF76>�X�R�A�ɉ������]��</color>")]
        [SerializeField] ScoreEvaluate[] _evaluate;

        public float TimeLimit => _timeLimit;
        public int FerverTime => _ferverTime;

        /// <summary>
        /// �X�R�A�ɉ������]����Ԃ�
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public string GetEvaluate(int score)
        {
            foreach (ScoreEvaluate eval in _evaluate.OrderByDescending(e => e.Score))
            {
                if (score >= eval.Score) return eval.Evaluate;
            }

            return "���݂��Ȃ��]��";
        }
    }
}
