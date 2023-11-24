using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// CRI�ŉ����Đ�����@�\�̃��b�p�[
    /// </summary>
    public static class Cri
    {
        public static void PlaySE(string name, string sheet = null)
        {
            if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play("CueSheet_SE", name);
            if (i == -1)
            {
                Debug.LogWarning("SE�����Ă��Ȃ�: " + name);
            }
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = null)
        {
            Debug.LogWarning("�x����SE����: " + name);
        }

        public static void PlayBGM(string name, string sheet = null)
        {
            int i = CriAudioManager.Instance.BGM.Play("CueSheet_BGM", name);
            if (i == -1)
            {
                Debug.LogWarning("BGM�����Ă��Ȃ�: " + name);
            }
        }

        public static void StopBGM()
        {
            CriAudioManager.Instance.BGM.StopAll();
        }

        public static void StopAll()
        {
            CriAudioManager.Instance.SE.StopAll();
            CriAudioManager.Instance.BGM.StopAll();
        }
    }
}
