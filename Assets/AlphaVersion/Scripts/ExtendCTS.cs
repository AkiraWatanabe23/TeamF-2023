using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// 2�ӏ��ȏ�œ���Token�ɑ΂���Dispose�����鎖���Ȍ��ɉ\�ɂ���
    /// CancellationTokenSource�ɋ@�\��ǉ������N���X
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
