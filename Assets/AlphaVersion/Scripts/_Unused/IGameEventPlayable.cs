using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    // 未使用

    /// <summary>
    /// ゲーム開始とゲーム終了の演出イベントを再生する機能のインターフェース
    /// </summary>
    public interface IGameEventPlayable
    {
        public UniTask PlayAsync(CancellationToken token, object arg = null);
    }
}