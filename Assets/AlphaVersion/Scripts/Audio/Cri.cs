using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// CRIで音を再生する機能のラッパー
    /// </summary>
    public static class Cri
    {
        const string SESheet = "CueSheet_SE5";
        const string BGMSheet = "CueSheet_BGM 4";
        const bool _valid = true; // 落ちるので無効化

        public static void PlaySE(string name)
        {
            if (!_valid) return;

            //if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play(SESheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SEが鳴っていない: " + name);
            }
        }

        public static void PlaySE3D(Vector3 position, string name)
        {
            if (!_valid) return;

            //if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play3D(position, SESheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SEが鳴っていない: " + name);
            }
        }

        public static void PlayBGM(string name)
        {
            if (!_valid) return;
            
            int i = CriAudioManager.Instance.BGM.Play(BGMSheet, name);
            if (i == -1)
            {
                Debug.LogWarning("BGMが鳴っていない: " + name);
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
