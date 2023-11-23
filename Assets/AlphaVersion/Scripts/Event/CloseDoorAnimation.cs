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
        [SerializeField] Transform _left;
        [SerializeField] Transform _right;
        [SerializeField] Transform _board;
        [SerializeField] float _duration;

        void Awake()
        {
            // 非表示にしておく
            _left.localScale = Vector3.zero;
            _right.localScale = Vector3.zero;
            _board.localScale = Vector3.zero;
        }

        /// <summary>
        /// ドアの閉まるアニメーションを再生し、終わるまで待つ
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _left.localScale = Vector3.one;
            _right.localScale = Vector3.one;
            _board.localScale = Vector3.one;

            _left.DOLocalRotate(Vector3.zero, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _right.DOLocalRotate(Vector3.zero, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
            
            _board.DOLocalMoveY(-0.25f, _duration).SetEase(Ease.InCubic).SetLink(gameObject);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_duration), cancellationToken: token);
        }
    }
}
