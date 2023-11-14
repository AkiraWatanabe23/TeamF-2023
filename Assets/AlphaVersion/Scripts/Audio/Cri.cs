using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// CRIで音を再生する機能のラッパー
    /// </summary>
    public static class Cri
    {
        public static void PlaySE(string name, string sheet = null)
        {
            TempAudioManager.Instance.PlaySE(name);
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = null)
        {
            TempAudioManager.Instance.DelayedPlaySE(name, delay);
        }

        public static void PlayBGM(string name, string sheet = null)
        {
            TempAudioManager.Instance.PlayBGM(name);
            
        }

        public static void StopBGM()
        {
            TempAudioManager.Instance.StopBGM();
        }
    }
}
