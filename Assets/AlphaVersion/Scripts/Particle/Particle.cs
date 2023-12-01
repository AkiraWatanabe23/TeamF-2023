using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    public enum ParticleType
    {
        Swoosh,
        Crash,
        Thun,
        Angry,
        Boo01,
        Boo02,
        Boo03,
        Good01,
        Good02,
        Happy01,
        Shock01,
        Clean,
        Hit02,
        Hit01,
    }

    /// <summary>
    /// パーティクル本体のクラス
    /// このスクリプトをアタッチするパーティクルはStopActionをNoneに設定する
    /// </summary>
    public class Particle : MonoBehaviour
    {
        [SerializeField] float _playTime;

        ParticleSystem[] _particleSystems;
        ParticlePool _pool; // プール

        /// <summary>
        /// 生成してプールに追加した際に1度だけプール側から呼び出されるメソッド
        /// </summary>
        public void OnCreate(ParticlePool pool)
        {
            _pool = pool;

            // 複数のパーティクルが親子関係になっていることを考慮して全て取得する
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
        }

        /// <summary>
        /// 外部からプールから取り出した際に再生を行うためのメソッド
        /// </summary>
        public void Play()
        {
            foreach (ParticleSystem ps in _particleSystems) ps.Play();

            DOVirtual.DelayedCall(_playTime, () => _pool.Return(this)).SetLink(gameObject);
        }
    }
}
