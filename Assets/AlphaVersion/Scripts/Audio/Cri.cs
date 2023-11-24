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
            if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play("CueSheet_SE", name);
            if (i == -1)
            {
                Debug.LogWarning("SEが鳴っていない: " + name);
            }
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = null)
        {
            Debug.LogWarning("遅延でSEが鳴る: " + name);
        }

        public static void PlayBGM(string name, string sheet = null)
        {
            int i = CriAudioManager.Instance.BGM.Play("CueSheet_BGM", name);
            if (i == -1)
            {
                Debug.LogWarning("BGMが鳴っていない: " + name);
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
