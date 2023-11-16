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
        [Header("引っ張った距離に応じた威力の増加具合")]
        [SerializeField] AnimationCurve _evaluate;
        [Header("投げる威力の倍率")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _power = 6.0f;
        [Header("最低威力")]
        [Range(0.1f, 20.0f)]
        [SerializeField] float _minPower = 1.0f;
        [Header("積める最大数")]
        [SerializeField] int _maxStack = 6;
        [Header("積む位置のランダムなずらし幅")]
        [Range(0, 0.1f)]
        [SerializeField] float _randomShift = 0.03f;
        [Header("積む際に強制的にアイテムを消す範囲")]
        [SerializeField] float _throwedAreaSqrRadius = 0.31f;

        public AnimationCurve Evaluate => _evaluate;
        public float Power => _power;
        public float MinPower => _minPower;
        public int MaxStack => _maxStack;
        public float RandomShift => _randomShift;
        public float ThrowedAreaSqrRadius => _throwedAreaSqrRadius;
    }
}
