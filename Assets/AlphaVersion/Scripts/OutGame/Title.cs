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

        void Start()
        {
            _library.ActiveChange();
            _libraryButton.onClick.AddListener(() => _library.ActiveChange());
        }
    }
}
