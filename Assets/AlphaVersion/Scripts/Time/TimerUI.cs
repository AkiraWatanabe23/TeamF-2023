using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̎��Ԍo�߂�\������N���X
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] Transform _gauge;

        void Awake()
        {
            _gauge.localScale = Vector3.one;
        }

        /// <summary>
        /// Transform��Scale��ύX���邱�ƂŎ��Ԍo�߂�\������
        /// </summary>
        public void Draw(float max, float current)
        {
            current = max - current;

            Vector3 scale = transform.localScale;
            scale.x = current / max;

            _gauge.localScale = scale;
        }
    }
}
