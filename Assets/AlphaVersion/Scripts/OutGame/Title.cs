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
        [SerializeField] Button _libraryButton;
        [SerializeField] LibraryContent _library;
        [Header("�}�Ӄ{�^�����N���b�N�����ۂɃA�j���[�V������҂�")]
        [SerializeField] float _delay = 1.5f;

        void Start()
        {
            _library.ActiveChange();
            _libraryButton.onClick.AddListener(() => StartCoroutine(ButtonAnimationAsync()));
        }

        IEnumerator ButtonAnimationAsync()
        {
            yield return new WaitForSeconds(_delay);
            _library.ActiveChange();
        }
    }
}
