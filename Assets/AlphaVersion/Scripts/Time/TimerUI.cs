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
        [SerializeField] Transform _background;
        [SerializeField] Image _ferverGauge;

        void Awake()
        {
            //_background.localScale = Vector3.one;

            //// �t�B�[�o�[�Q�[�W�̒����ݒ�
            //float f = _settings.FerverTime / _settings.TimeLimit;
            //_ferverGauge.transform.localScale = new Vector3(f, 1, 1);
        }

        void Update()
        {
            // �t�B�[�o�[�̃Q�[�W����F�ɂ���
            //_ferverGauge.color = Color.HSVToRGB(Time.time % 1, 1, 1);
        }

        /// <summary>
        /// Transform��Scale��ύX���邱�ƂŎ��Ԍo�߂�\������
        /// </summary>
        public void Draw(float max, float current)
        {
            //current = max - current;

            //Vector3 scale = transform.localScale;
            //scale.x = 1.0f - (current / max);

            //_background.localScale = scale;
        }
    }
}
