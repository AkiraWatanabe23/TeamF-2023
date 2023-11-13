using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// 現在の合計スコアの読み取りが出来るインターフェース
    /// </summary>
    public interface ITotalScoreReader
    {
        public IReadOnlyReactiveProperty<int> TotalScore { get; }
    }
}
