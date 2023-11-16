using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// インゲームの設定
    /// </summary>
    [CreateAssetMenu(fileName = "InGameSettingsSO", menuName = "Settings/InGameSettings")]
    public class InGameSettingsSO : ScriptableObject
    {
        [Header("制限時間")]
        [SerializeField] int _timeLimit = 60;
        [Header("フィーバータイム開始")]
        [SerializeField] int _ferverTime = 20;

        public float TimeLimit => _timeLimit;
        public int FerverTime => _ferverTime;    }
}
