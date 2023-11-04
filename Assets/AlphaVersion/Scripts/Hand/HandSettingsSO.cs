using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 手の設定
    /// </summary>
    [CreateAssetMenu(fileName = "HandSettingsSO", menuName = "Settings/HandSettings")]
    public class HandSettingsSO : ScriptableObject
    {
        [System.Serializable]
        public struct Offset
        {
            const float Range = 0.2f;

            [Range(-Range, Range)]
            [SerializeField] float x;
            [Range(-Range, Range)]
            [SerializeField] float y;
            [Range(-Range, Range)]
            [SerializeField] float z;
            [Header("ランダム性")]
            [Range(0, Range)]
            [SerializeField] float _random;

            public Vector3 Position
            {
                get
                {
                    Vector3 p = new Vector3(x, y, z);
                    //p.y += Random.Range(-_random, _random);
                    p.z += Random.Range(-_random, _random);
                    return p;
                }
            }
        }

        [Header("物を置くパーティクルの位置のオフセット")]
        [SerializeField] Offset _thunParticleOffset;
        [Header("滑っていくパーティクルの位置のオフセット")]
        [SerializeField] Offset _swooshParticleOffset;
        [Header("滑っていくパーティクルがアイテムを追従する")]
        [SerializeField] bool _swooshParticleItemFollow = true;
        [Header("グラスが割れるパーティクルの位置のオフセット")]
        [SerializeField] Offset _crashParticleOffset;

        public Vector3 StackParticleOffset => _thunParticleOffset.Position;
        public Vector3 ThrowParticleOffset => _swooshParticleOffset.Position;
        public Vector3 CrashParticleOffset => _crashParticleOffset.Position;
        public bool SwooshParticleItemFollow => _swooshParticleItemFollow;
    }
}
