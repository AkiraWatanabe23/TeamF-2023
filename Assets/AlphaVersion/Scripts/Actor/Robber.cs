using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �����̃N���X
    /// </summary>
    public class Robber : Actor
    {
        [Header("�X�e�[�g")]
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
