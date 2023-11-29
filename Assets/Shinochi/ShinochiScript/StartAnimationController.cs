using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartAnimationController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    void Update()
    {
        // スペースボタンが押されたらanimation再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeline.Play();
        }
    }
}
