using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// 選択するアイテムそれぞれのアイコンUIのクラス
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemFrameUI : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] Image _frame;
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] Sprite _selected;
        [SerializeField] Sprite _unSelected;

        /// <summary>
        /// このUIに割り当てるアイコンを設定して有効化
        /// </summary>
        public void Valid(Sprite icon)
        {
            _icon.sprite = icon;
            Deselect();
        }

        /// <summary>
        /// このUIに対応するアイテムが無いので無効化
        /// </summary>
        public void InValid()
        {
            _canvasGroup.alpha = 0;
        }

        /// <summary>
        /// このアイテムを選択した
        /// </summary>
        public void Select()
        {
            _frame.sprite = _selected;
        }

        /// <summary>
        /// 他のアイテムを選択した
        /// </summary>
        public void Deselect()
        {
            _frame.sprite = _unSelected;
        }
    }
}
