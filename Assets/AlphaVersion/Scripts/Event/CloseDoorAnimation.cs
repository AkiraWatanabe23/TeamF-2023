using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ゲームオーバー時のドアが閉まるアニメーション
    /// (0,0,0)に向けて回転して閉まるので、LeftとRightの親がカメラと同じ向きを向いている必要がある。
    /// </summary>
    public class CloseDoorAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] float _duration = 3.0f;

        void Start()
        {
            _animator.transform.localScale = Vector3.zero;
        }

        /// <summary>
        /// ドアの閉まるアニメーションを再生し、終わるまで待つ
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _animator.transform.localScale = Vector3.one;
            _animator.enabled = true;

            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
        }
    }
}
