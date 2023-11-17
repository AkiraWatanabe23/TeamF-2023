using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーション再生のクラスを、キャラクターの振る舞いを制御するクラスが
/// 直接参照しないように接続するためのクラス
/// </summary>
public class AnimationAdapter : MonoBehaviour
{
    // TODO:岩垂君のアニメーション再生スクリプトが良い感じになったら切り替える
    [SerializeField] Animator _animator;

    public void Play(string name)
    {
        _animator.Play(name);
    }
}
