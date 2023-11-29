using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 追加
public class ChengeScene : MonoBehaviour
{
    [SerializeField] private Animator _animator; // アニメーターへの参照
    [SerializeField] private string _animationName;
    [SerializeField]
    private SceneName _loadScene = SceneName.Title;

    private void Start()
    {
        // ゲームの状態をリセット
        //PlayerPrefs.DeleteAll();
    }
    public void StartChangeScene()
    {
        StartCoroutine(PlayAnimationAndChangeScene());
    }

    private IEnumerator PlayAnimationAndChangeScene()
    {
        _animator.SetTrigger(_animationName); // アニメーションの開始

        // アニメーションの長さを取得
        float animationLength = GetAnimationLength(_animationName);

        // アニメーションが終了するのを待つ
        yield return new WaitForSeconds(animationLength);

        SceneManager.LoadScene(Consts.Scenes[_loadScene]); // シーン遷移
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
        // アニメーションが見つからない場合やエラーの場合は、適当なデフォルト値を返す
        return 2.0f;
    }
}
