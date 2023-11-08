using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// 強盗の生成処理を登録可能なインターフェース
    /// </summary>
    public interface IRobberSpawnRegisterable
    {
        public event UnityAction OnRobberSpawned;
    }
}
