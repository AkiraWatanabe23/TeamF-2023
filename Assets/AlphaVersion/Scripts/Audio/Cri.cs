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
            CriAudioManager.Instance.SE.Play("CueSheet_SE", name);
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = null)
        {
            //TempAudioManager.Instance.DelayedPlaySE(name, delay);
        }

        public static void PlayBGM(string name, string sheet = null)
        {
            //TempAudioManager.Instance.PlayBGM(name);
            
        }

        public static void StopBGM()
        {
            //TempAudioManager.Instance.StopBGM();
        }
    }
}
