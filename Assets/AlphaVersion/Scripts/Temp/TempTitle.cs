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
        [Header("�}�ӂ̃��C�A�E�g��2��")]
        [SerializeField] GameObject _canvas1;
        [SerializeField] GameObject _canvas2;
        [SerializeField] Button _pictureBookButton1;
        [SerializeField] Button _pictureBookButton2;
        [SerializeField] Button _closePictureBookButton1;
        [SerializeField] Button _closePictureBookButton2;

        void Awake()
        {
            _playButton.onClick.AddListener(ToInGameScene);
            PictureBook();
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

        /// <summary>
        /// �}�ӂ̃��C�A�E�g��2��ނ��{�^���N���b�N�ŕ\��
        /// </summary>
        void PictureBook()
        {
            _canvas1.SetActive(false);
            _canvas2.SetActive(false);

            _pictureBookButton1.onClick.AddListener(() => _canvas1.SetActive(true));
            _pictureBookButton2.onClick.AddListener(() => _canvas2.SetActive(true));
            _closePictureBookButton1.onClick.AddListener(() => _canvas1.SetActive(false));
            _closePictureBookButton2.onClick.AddListener(() => _canvas2.SetActive(false));
        }
    }
}
