using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// �����̐���������o�^�\�ȃC���^�[�t�F�[�X
    /// </summary>
    public interface IRobberSpawnRegisterable
    {
        public event UnityAction OnRobberSpawned;
    }
}
