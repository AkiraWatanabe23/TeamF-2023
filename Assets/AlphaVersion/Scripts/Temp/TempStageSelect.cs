using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Alpha
{
    /// <summary>
    /// �X�e�[�W�Z���N�g��ʂ̉��̃X�N���v�g
    /// </summary>
    public class TempStageSelect : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public Button _button;
            public string _name;
        }

        [SerializeField] Data[] _data;

        void Awake()
        {
            TryFadeIn();
            Register();
        }

        /// <summary>
        /// �{�^���ɃV�[���J�ڂ̏�����o�^
        /// </summary>
        void Register()
        {
            foreach (Data data in _data)
            {
                data._button.onClick.AddListener(() => ToStageScene(data._name));
            }
        }

        /// <summary>
        /// �t�F�[�h�p�̃X�N���v�g������ꍇ�̓t�F�[�h�C������
        /// </summary>
        void TryFadeIn()
        {
            if (Fade.Instance != null) Fade.Instance.StartFadeIn();
        }

        /// <summary>
        /// �I�������X�e�[�W�ɑJ�ڂ���
        /// </summary>
        void ToStageScene(string sceneName)
        {
            if (Fade.Instance == null)
            {
                Debug.LogWarning("�t�F�[�h�p�̃X�N���v�g��������Ȃ�����");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Fade.Instance.StartFadeOut(() => SceneManager.LoadScene(sceneName));
            }
        }
    }
}
