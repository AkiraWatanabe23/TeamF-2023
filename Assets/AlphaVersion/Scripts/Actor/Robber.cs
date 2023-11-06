using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 強盗のクラス
    /// </summary>
    public class Robber : Actor
    {
        [Header("ステート")]
        [SerializeField] MoveState _moveState;

        BaseState _currentState;

        protected override void OnInitOverride(Waypoint lead, TableManager _, Tension tension)
        {
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
        }
    }
}
