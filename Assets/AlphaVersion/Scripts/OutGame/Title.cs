using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

namespace Alpha
{
    public class Title : MonoBehaviour
    {
        [SerializeField] Button _titleButton;
        [SerializeField] Button _libraryButton;
        [SerializeField] IllustratedBook _library;
        [Header("図鑑ボタンをクリックした際にアニメーションを待つ")]
        [SerializeField] float _delay = 1.5f;

        void Start()
        {
            _titleButton.onClick.AddListener(PlayTitleButtonSE);
            _library.ActiveChange();
            _libraryButton.onClick.AddListener(() => StartCoroutine(ButtonAnimationAsync()));
        }

        IEnumerator ButtonAnimationAsync()
        {
            yield return new WaitForSeconds(_delay);
            _library.ActiveChange();
        }

        public void PlayTitleButtonSE()
        {
            Cri.PlaySE("SE_Decision");
        }
    }
}
