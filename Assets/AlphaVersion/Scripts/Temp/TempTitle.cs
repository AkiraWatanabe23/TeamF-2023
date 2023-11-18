using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Alpha
{
    /// <summary>
    /// �^�C�g����ʂ̉��̃X�N���v�g
    /// </summary>
    public class TempTitle : MonoBehaviour
    {
        [SerializeField] Button _playButton;
        [SerializeField] string _inGameSceneName = "Stage_1_Normal";

        void Awake()
        {
            _playButton.onClick.AddListener(ToInGameScene);
        }

        /// <summary>
        /// �{���Ȃ�X�e�[�W�I���ɔ�Ԃ��A�Ƃ肠�����C���Q�[���ɔ��
        /// �t�F�[�h�p�̃X�N���v�g������΃t�F�[�h���Ĕ��
        /// </summary>
        void ToInGameScene()
        {
            if (Fade.Instance == null)
            {
                Debug.LogWarning("�t�F�[�h�p�̃X�N���v�g��������Ȃ�����");
                SceneManager.LoadScene(_inGameSceneName);
            }
            else
            {
                Fade.Instance.StartFadeOut(() => SceneManager.LoadScene(_inGameSceneName));
            }
        }
    }
}
