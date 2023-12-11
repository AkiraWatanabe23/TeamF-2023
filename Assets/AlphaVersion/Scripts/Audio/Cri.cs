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
        const bool _valid = false; // 落ちるので無効化

        public static void PlaySE(string name, string sheet = "CueSheet_SE")
        {
            if (!_valid) return;

            if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play(sheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SEが鳴っていない: " + name);
            }
        }

        public static void PlaySE3D(Vector3 position, string name, string sheet = "CueSheet_SE")
        {
            if (!_valid) return;

            if (CriAudioManager.Instance == null) return;

            int i = CriAudioManager.Instance.SE.Play3D(position, sheet, name);
            if (i == -1)
            {
                Debug.LogWarning("SEが鳴っていない: " + name);
            }
        }

        public static void DelayedPlaySE(string name, float delay, string sheet = "CueSheet_SE")
        {
            if (!_valid) return;

            DOVirtual.DelayedCall(delay, () => PlaySE(name, sheet));
        }

        public static void PlayBGM(string name, string sheet = "CueSheet_BGM")
        {
            if (!_valid) return;

            int i = CriAudioManager.Instance.BGM.Play(sheet, name);
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
