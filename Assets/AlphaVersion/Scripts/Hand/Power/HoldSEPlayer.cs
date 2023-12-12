using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムの力を調整中に鳴らす音を再生する機能のクラス
    /// </summary>
    public class HoldSEPlayer
    {
        // 1度だけ音を再生するためのフラグ
        bool _unPlayed;

        /// <summary>
        /// 投げる力を加え続けている状態
        /// </summary>
        public void HoldOn()
        {
            if (_unPlayed)
            {
                Cri.PlaySE("仮の投げる音");
                _unPlayed = false;
            }
        }

        /// <summary>
        /// 投げる力を加えていない状態
        /// </summary>
        public void HoldOff()
        {
            _unPlayed = true;
        }
    }
}