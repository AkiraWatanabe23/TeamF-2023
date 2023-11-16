using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// キャッチ判定に関する設定
    /// </summary>
    [CreateAssetMenu(fileName = "CatchSettingsSO", menuName = "Settings/CatchSettings")]
    [System.Serializable]
    public class CatchSettingsSO : ScriptableObject
    {
        [Header("キャッチ判定の大きさの設定")]
        [Range(0.1f, 0.8f)]
        [SerializeField] float _normalSize = 0.25f;
        [Range(0.1f, 0.8f)]
        [SerializeField] float _ferverSize = 0.8f;
        [Header("商品をキャッチできる速度")]
        [SerializeField] float _catchableSpeed = 1.0f;

        public float NormalSize => _normalSize;
        public float FerverSize => _ferverSize;
        public float CatchableSpeed => _catchableSpeed;
    }
}
