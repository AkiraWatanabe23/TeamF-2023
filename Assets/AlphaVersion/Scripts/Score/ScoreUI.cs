using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// �X�R�A��UI�ɕ\������@�\�̃N���X
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] Text _valueTextUI;

        public void Draw(int score)
        {
            // UI�ւ̕\��
            _valueTextUI.text = score.ToString();
        }
    }
}
