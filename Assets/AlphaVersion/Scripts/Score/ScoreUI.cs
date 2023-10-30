using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// スコアをUIに表示する機能のクラス
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] Text _valueTextUI;

        public void Draw(int score)
        {
            // UIへの表示
            _valueTextUI.text = score.ToString();
        }
    }
}
