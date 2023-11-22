using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����Đ��̃N���X���A�L�����N�^�[�̐U�镑���𐧌䂷��N���X��
/// ���ڎQ�Ƃ��Ȃ��悤�ɐڑ����邽�߂̃N���X
/// </summary>
public class AnimationAdapter : MonoBehaviour
{
    [SerializeField] RobotAnimationScripts _animation;

    /// <summary>
    /// �A�j���[�V�������Đ�
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
    /// �Ȃ�\��
    /// </summary>
    public void ReservedTable(int index)
    {
        _animation.SitReceipt(index);
    }
}
