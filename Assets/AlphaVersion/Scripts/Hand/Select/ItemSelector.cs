using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Alpha
{
    /// <summary>
    /// ���͂ɑΉ������A�C�e����I������@�\�̃N���X
    /// </summary>
    public class ItemSelector
    {
        int _currentIndex;
        
        int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                
                // �A�C�e���̐��͈̔͂ɃN�����v����
                int length = Enum.GetValues(typeof(ItemType)).Length;
                _currentIndex = Mathf.Clamp(_currentIndex, 0, length - 1);

                // �I�������A�C�e�������b�Z�[�W���O����
                ItemMessageSender.SendMessage((ItemType)CurrentIndex);
            }
        }

        /// <summary>
        /// �L�[���͂őI�����s��
        /// �Ή������L�[(ASDFG)�̏ꍇ�̂ݕύX���A����ȊO�̏ꍇ�͌��݂̓Y�������ێ�����
        /// </summary>
        public ItemType Select(KeyCode key)
        {
            // TODO:�L�[��Y�����ɑΉ������Ă��邾���Ȃ̂ŁA�A�C�e���̎�ނ����������ꍇ�͏C�����K�v
            if      (key == KeyCode.A) CurrentIndex = 0;
            else if (key == KeyCode.S) CurrentIndex = 1;
            else if (key == KeyCode.D) CurrentIndex = 2;
            else if (key == KeyCode.F) CurrentIndex = 3;
            else if (key == KeyCode.G) CurrentIndex = 4;

            return (ItemType)CurrentIndex;
        }

        /// <summary>
        /// �}�E�X�z�C�[���őI�����s��
        /// </summary>
        public ItemType Select(float fov)
        {
            if (fov > 0) CurrentIndex--;
            if (fov < 0) CurrentIndex++;

            return (ItemType)CurrentIndex;
        }
    }
}

