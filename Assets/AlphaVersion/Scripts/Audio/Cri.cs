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
        public static void PlaySE(string name, string sheet = "CueSheet_SE")
        {
            int i = CriAudioManager.Instance.SE.Play(sheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SE�����Ă��Ȃ�: " + name);
            }
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = "CueSheet_SE")
        {
            DOVirtual.DelayedCall(delay, () => PlaySE(name, sheet));
        }

        public static void PlayBGM(string name, string sheet = "CueSheet_BGM")
        {
            int i = CriAudioManager.Instance.BGM.Play(sheet, name);
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
