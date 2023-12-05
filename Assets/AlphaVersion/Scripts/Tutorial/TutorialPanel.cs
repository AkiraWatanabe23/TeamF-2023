using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] GameObject _panel;

        void Awake()
        {
            MessageBroker.Default.Receive<GameOverMessage>()
                .Subscribe(_ => _panel.SetActive(false)).AddTo(gameObject);
        }
    }
}