using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    public class ButtonSpriteSwitch : MonoBehaviour
    {
        [SerializeField] Image _button;
        [SerializeField] Sprite _default;
        [SerializeField] Sprite _pushed;

        public void Push()
        {
            _button.sprite = _pushed;
        }

        public void Default()
        {
            _button.sprite = _default;
        }
    }
}
