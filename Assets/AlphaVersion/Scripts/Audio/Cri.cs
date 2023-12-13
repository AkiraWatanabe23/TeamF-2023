using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// CRI�ŉ����Đ�����@�\�̃��b�p�[
    /// </summary>
    public static class Cri
    {
        const string SESheet = "CueSheet_SE5";
        const string BGMSheet = "CueSheet_BGM 4";
        const bool _valid = true; // ������̂Ŗ�����

        public static void PlaySE(string name)
        {
            if (!_valid) return;

            //if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play(SESheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SE�����Ă��Ȃ�: " + name);
            }
        }

        public static void PlaySE3D(Vector3 position, string name)
        {
            if (!_valid) return;

            //if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play3D(position, SESheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SE�����Ă��Ȃ�: " + name);
            }
        }

        public static void PlayBGM(string name)
        {
            if (!_valid) return;
            
            int i = CriAudioManager.Instance.BGM.Play(BGMSheet, name);
            if (i == -1)
            {
                Debug.LogWarning("BGM�����Ă��Ȃ�: " + name);
            }
        }

        public static void StopBGM()
        {
            if (!_valid) return;

            CriAudioManager.Instance.BGM.StopAll();
        }

        public static void StopAll()
        {
            if (!_valid) return;

            CriAudioManager.Instance.SE.StopAll();
            CriAudioManager.Instance.BGM.StopAll();
        }
    }
}
