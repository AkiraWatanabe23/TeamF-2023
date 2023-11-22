using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ライトを回転させるアニメーション
    /// 複数のライトを回転させるため、ボトルネックになる可能性が高い
    /// </summary>
    public class LightRotate : MonoBehaviour
    {
        [SerializeField] float _duration = 5.0f;
        [Header("回す対象")]
        [SerializeField] Transform _spotLight;
        [SerializeField] Transform _mirrorBall;

        /// <summary>
        /// 回転のアニメーションを再生
        /// </summary>
        public void Play()
        {
            _spotLight.DORotate(new Vector3(0, 360, 0), _duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);

            _mirrorBall.DORotate(new Vector3(0, 360, 0), _duration / 10, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);
        }
    }
}