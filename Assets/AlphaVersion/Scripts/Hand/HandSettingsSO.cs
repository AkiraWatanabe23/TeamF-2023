using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 手の設定
    /// </summary>
    [CreateAssetMenu(fileName = "HandSettingsSO", menuName = "Settings/HandSettings")]
    [System.Serializable]
    public class HandSettingsSO : ScriptableObject
    {
        /// <summary>
        /// フィーバータイム中にミニキャラクターを投げる確率
        /// プランナーに弄らせる場合は尻化する
        /// </summary>
        public readonly float FerverMiniActorPercent = float.MaxValue;

        [Header("<color=#00FF76>引っ張った距離に応じた威力の増加具合</color>")]
        [SerializeField] AnimationCurve _evaluate;
        [Header("<color=#00FF76>投げる威力の倍率</color>")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _power = 6.0f;
        [Header("<color=#00FF76>最低威力</color>")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _minPower = 1.0f;
        [Header("<color=#00FF76>積める最大数</color>")]
        [Range(1, 6)]
        [SerializeField] int _maxStack = 6;
        [Header("<color=#00FF76>積む位置のランダムなずらし幅</color>")]
        [Range(0, 0.1f)]
        [SerializeField] float _randomShift = 0.03f;
        [Header("<color=#00FF76>積む際に強制的にアイテムを消す範囲</color>")]
        [SerializeField] float _throwedAreaSqrRadius = 0.31f;
        [Header("<color=#00FF76>強盗の攻撃を受けた際のペナルティ(秒)</color>")]
        [SerializeField] float _damagedPenalty = 2.0f;

        public AnimationCurve Evaluate => _evaluate;
        public float Power => _power;
        public float MinPower => _minPower;
        public int MaxStack => _maxStack;
        public float RandomShift => _randomShift;
        public float ThrowedAreaSqrRadius => _throwedAreaSqrRadius;
        public float DamagedPenalty => _damagedPenalty;
    }
}
