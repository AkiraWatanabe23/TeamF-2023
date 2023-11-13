using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムのパーティクルを制御するクラス
    /// このパーティクルはプーリングされず、シーン開始時に生成済みのものを再生/停止を行う
    /// </summary>
    public class FerverEffect : FerverHandler
    {
        [SerializeField] ParticleSystem _left;
        [SerializeField] ParticleSystem _right;

        protected override void OnAwakeOverride()
        {
        }

        protected override void OnFerverTimeEnter()
        {
            _left.Play();
            _right.Play();
        }

        protected override void OnFerverTimeExit()
        {
            _left.Stop();
            _right.Stop();
        }
    }
}
