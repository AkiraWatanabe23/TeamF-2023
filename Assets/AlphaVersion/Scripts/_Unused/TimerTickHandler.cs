using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̎��Ԍo�߂��n���h������N���X
    /// </summary>
    public class TimerTickHandler : MonoBehaviour, ITickReceive
    {
        [SerializeField] TimerUI _timerUI;

        /// <summary>
        /// ���Ԃ̍X�V
        /// </summary>
        public void Tick(float max, float current)
        {
            _timerUI.Draw(max, current);
        }
    }
}