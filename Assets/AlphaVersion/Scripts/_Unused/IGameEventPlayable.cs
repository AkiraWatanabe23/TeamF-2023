using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    // ���g�p

    /// <summary>
    /// �Q�[���J�n�ƃQ�[���I���̉��o�C�x���g���Đ�����@�\�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IGameEventPlayable
    {
        public UniTask PlayAsync(CancellationToken token, object arg = null);
    }
}