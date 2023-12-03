using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// �I������A�C�e�����ꂼ��̃A�C�R��UI�̃N���X
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
        /// ����UI�Ɋ��蓖�Ă�A�C�R����ݒ肵�ėL����
        /// </summary>
        public void Valid(Sprite icon)
        {
            _icon.sprite = icon;
            Deselect();
        }

        /// <summary>
        /// ����UI�ɑΉ�����A�C�e���������̂Ŗ�����
        /// </summary>
        public void InValid()
        {
            _canvasGroup.alpha = 0;
        }

        /// <summary>
        /// ���̃A�C�e����I������
        /// </summary>
        public void Select()
        {
            _frame.sprite = _selected;
        }

        /// <summary>
        /// ���̃A�C�e����I������
        /// </summary>
        public void Deselect()
        {
            _frame.sprite = _unSelected;
        }
    }
}
