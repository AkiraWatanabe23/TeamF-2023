using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartAnimationController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ��animation�Đ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeline.Play();
        }
    }
}
