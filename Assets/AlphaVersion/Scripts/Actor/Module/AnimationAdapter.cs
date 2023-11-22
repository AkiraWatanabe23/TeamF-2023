using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーション再生のクラスを、キャラクターの振る舞いを制御するクラスが
/// 直接参照しないように接続するためのクラス
/// </summary>
public class AnimationAdapter : MonoBehaviour
{
    [SerializeField] RobotAnimationScripts _animation;

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    public void Play(string name)
    {
        if (name == "Walk") _animation.WalkAnimation();
        else if (name == "Idle") _animation.IdleState();
        else if (name == "Order") _animation.SitAnimation();
        else if (name == "Success") _animation.SuccessAnimation();
        else if (name == "Failure") _animation.FailedAnimation();
        else if (name == "Dance") _animation.DanceAnimation();
    }

    /// <summary>
    /// 席を予約
    /// </summary>
    public void ReservedTable(int index)
    {
        _animation.SitReceipt(index);
    }
}
