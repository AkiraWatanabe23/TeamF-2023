using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����Đ��̃N���X���A�L�����N�^�[�̐U�镑���𐧌䂷��N���X��
/// ���ڎQ�Ƃ��Ȃ��悤�ɐڑ����邽�߂̃N���X
/// </summary>
public class AnimationAdapter : MonoBehaviour
{
    // TODO:�␂�N�̃A�j���[�V�����Đ��X�N���v�g���ǂ������ɂȂ�����؂�ւ���
    [SerializeField] Animator _animator;

    public void Play(string name)
    {
        _animator.Play(name);
    }
}
