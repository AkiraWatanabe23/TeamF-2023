using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���̗͂𒲐����ɖ炷�����Đ�����@�\�̃N���X
    /// </summary>
    public class HoldSEPlayer
    {
        // 1�x���������Đ����邽�߂̃t���O
        bool _unPlayed;

        /// <summary>
        /// ������͂����������Ă�����
        /// </summary>
        public void HoldOn()
        {
            if (_unPlayed)
            {
                Cri.PlaySE("���̓����鉹");
                _unPlayed = false;
            }
        }

        /// <summary>
        /// ������͂������Ă��Ȃ����
        /// </summary>
        public void HoldOff()
        {
            _unPlayed = true;
        }
    }
}