using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �q���L���b�`�\�ȃA�C�e���̃C���^�[�t�F�[�X
    /// ���������A�C�e�����A���x�����ȉ��̏ꍇ�̓L���b�`�����
    /// </summary>
    public interface ICatchable
    {
        /// <summary>
        /// ���������A�C�e�����ǂ����𔻒肷��̂Ɏg�p����
        /// </summary>
        public ItemType Type { get; }

        /// <summary>
        /// �L���b�`�G���A�����̃A�C�e�����L���b�`�������𔻒肷�邽�߂Ɏg�p����
        /// </summary>
        public float SqrSpeed { get; }

        /// <summary>
        /// �L���b�`�����ۂɌĂ΂��
        /// </summary>
        public void OnCatched();
    }
}
