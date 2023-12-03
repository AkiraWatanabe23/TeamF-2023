using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Alpha
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] string _nextSceneName;

        public void Transition()
        {
            Fade.Instance.StartFadeOut(() =>
            {
                SceneManager.LoadScene(_nextSceneName);
            });
        }
    }
}
