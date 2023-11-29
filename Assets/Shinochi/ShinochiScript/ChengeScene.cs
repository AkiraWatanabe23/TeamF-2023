using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �ǉ�
public class ChengeScene : MonoBehaviour
{
    [SerializeField] private Animator _animator; // �A�j���[�^�[�ւ̎Q��
    [SerializeField] private string _animationName;
    [SerializeField]
    private SceneName _loadScene = SceneName.Title;

    private void Start()
    {
        // �Q�[���̏�Ԃ����Z�b�g
        //PlayerPrefs.DeleteAll();
    }
    public void StartChangeScene()
    {
        StartCoroutine(PlayAnimationAndChangeScene());
    }

    private IEnumerator PlayAnimationAndChangeScene()
    {
        _animator.SetTrigger(_animationName); // �A�j���[�V�����̊J�n

        // �A�j���[�V�����̒������擾
        float animationLength = GetAnimationLength(_animationName);

        // �A�j���[�V�������I������̂�҂�
        yield return new WaitForSeconds(animationLength);

        SceneManager.LoadScene(Consts.Scenes[_loadScene]); // �V�[���J��
    }

    private float GetAnimationLength(string animationName)
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(animationName))
            {
                return clip.length;
            }
        }
        // �A�j���[�V������������Ȃ��ꍇ��G���[�̏ꍇ�́A�K���ȃf�t�H���g�l��Ԃ�
        return 2.0f;
    }
}
