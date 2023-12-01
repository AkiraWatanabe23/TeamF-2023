using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    public enum ParticleType
    {
        Swoosh,
        Crash,
        Thun,
        Angry,
        Boo01,
        Boo02,
        Boo03,
        Good01,
        Good02,
        Happy01,
        Shock01,
        Clean,
        Hit02,
        Hit01,
    }

    /// <summary>
    /// �p�[�e�B�N���{�̂̃N���X
    /// ���̃X�N���v�g���A�^�b�`����p�[�e�B�N����StopAction��None�ɐݒ肷��
    /// </summary>
    public class Particle : MonoBehaviour
    {
        [SerializeField] float _playTime;

        ParticleSystem[] _particleSystems;
        ParticlePool _pool; // �v�[��

        /// <summary>
        /// �������ăv�[���ɒǉ������ۂ�1�x�����v�[��������Ăяo����郁�\�b�h
        /// </summary>
        public void OnCreate(ParticlePool pool)
        {
            _pool = pool;

            // �����̃p�[�e�B�N�����e�q�֌W�ɂȂ��Ă��邱�Ƃ��l�����đS�Ď擾����
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
        }

        /// <summary>
        /// �O������v�[��������o�����ۂɍĐ����s�����߂̃��\�b�h
        /// </summary>
        public void Play()
        {
            foreach (ParticleSystem ps in _particleSystems) ps.Play();

            DOVirtual.DelayedCall(_playTime, () => _pool.Return(this)).SetLink(gameObject);
        }
    }
}
