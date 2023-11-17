using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// 2箇所以上で同じTokenに対してDisposeをする事を簡潔に可能にした
    /// CancellationTokenSourceに機能を追加したクラス
    /// </summary>
    public class ExtendCTS
    {
        CancellationTokenSource _cts;

        public ExtendCTS()
        {
            _cts = new();
        }

        public CancellationToken Token => _cts.Token;

        public void Dispose()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}
