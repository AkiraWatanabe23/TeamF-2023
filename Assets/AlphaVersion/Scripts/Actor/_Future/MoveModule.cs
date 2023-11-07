using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// 前後の地点に移動する機能のクラス
    /// 現在の地点の情報しか保持していないので、次にどの地点に移動すれば
    /// 良いのかを外部で管理し、移動する際に渡してやる必要がある。
    /// </summary>
    public class MoveModule : MonoBehaviour
    {
        [SerializeField] MoveBetween _moveBetween;

        Waypoint _current;
        ActorSettingsSO _settings;
        CancellationTokenSource _cts;

        /// <summary>
        /// 経路の先頭を渡すことで初期化する
        /// </summary>
        public void Init(Waypoint lead, ActorSettingsSO settings)
        {
            _current = lead;
            _settings = settings;

            // 経路の先頭に初期配置する
            transform.position = lead.Position;
        }

        /// <summary>
        /// 次の地点まで移動する、最後の地点の場合はその場に移動
        /// 次の地点が複数ある場合は先頭の地点を選択するので注意
        /// </summary>
        public async UniTask MoveToNextAsync(int index = 0)
        {
            Waypoint next = _current.IsFinal ? _current : _current.Next[index];
            _current = await MoveToAsync(next, _cts.Token);
        }

        /// <summary>
        /// 前の地点まで移動する、最初の地点の場合はその場に移動
        /// 前の地点が複数ある場合は先頭の地点を選択するので注意
        /// </summary>
        public async UniTask MoveToPrevAsync(int index = 0)
        {
            Waypoint prev = _current.IsLead ? _current : _current.Prev[index];
            _current = await MoveToAsync(prev, _cts.Token);
        }

        /// <summary>
        /// 現在の位置から指定した地点まで移動する
        /// </summary>
        /// <returns>次の地点</returns>
        async UniTask<Waypoint> MoveToAsync(Waypoint to, CancellationToken token)
        {
            await _moveBetween.MoveAsync(_settings.MoveSpeed, transform.position, to.Position, token);
            return to; // 移動完了時に現在の地点を更新
        }
    }
}


// 問題提起:次/前のウェイポイントが複数あった場合どうする？外部から指定しないといけない。
// 次に移動->キャンセル->次に移動 だとバグらない？
// 