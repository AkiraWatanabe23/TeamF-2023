using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// ���݂̍��v�X�R�A�̓ǂݎ�肪�o����C���^�[�t�F�[�X
    /// </summary>
    public interface ITotalScoreReader
    {
        public IReadOnlyReactiveProperty<int> TotalScore { get; }
    }
}
