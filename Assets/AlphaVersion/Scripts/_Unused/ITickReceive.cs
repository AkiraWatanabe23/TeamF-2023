using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̃^�C�}�[���i�s����x�ɏ������s���@�\�̃C���^�[�t�F�[�X
    /// </summary>
    public interface ITickReceive
    {
        public void Tick(float max, float current);
    }
}
