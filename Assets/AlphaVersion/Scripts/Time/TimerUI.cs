using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[���̎��Ԍo�߂�\������N���X
    /// �w�i�̑傫����0����1�ɕύX���邱�ƂŃQ�[�W���}�X�N���A�c�莞�Ԃ�\��
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] Image _feverGauge;
        [SerializeField] Transform _under;
        [SerializeField] Transform _ferverUnder;
        [SerializeField] float _mag = 1.0f;
        [SerializeField] float _feverMag = 1.0f;

        void Update()
        {
            if (_ferverUnder.localScale.x > 0)
            {
                // �t�B�[�o�[�̃Q�[�W����F�ɂ���
                _feverGauge.color = Color.HSVToRGB(Time.time % 1, 1, 1);
            }
        }

        /// <summary>
        /// Transform��Scale��ύX���邱�ƂŎ��Ԍo�߂�\������
        /// </summary>
        public void Draw(float max, float current)
        {
            //Debug.Log(current + " " + max);

            if (_under.localScale.x <= 1)
            {
                Vector3 scale = _under.localScale;
                scale.x += Time.deltaTime * _mag;
                _under.localScale = scale;
            }
            else
            {
                Vector3 scale = _ferverUnder.localScale;
                scale.x += Time.deltaTime * _feverMag;
                _ferverUnder.localScale = scale;
            }
        }
    }
}
