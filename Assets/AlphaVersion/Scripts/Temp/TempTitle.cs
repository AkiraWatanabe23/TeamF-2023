using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Alpha
{
    /// <summary>
    /// タイトル画面の仮のスクリプト
    /// </summary>
    public class TempTitle : MonoBehaviour
    {
        [SerializeField] Button _playButton;
        [SerializeField] string _inGameSceneName = "Stage_1_Normal";
        [Header("図鑑のレイアウト案2つ")]
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
        /// 本来ならステージ選択に飛ぶが、とりあえずインゲームに飛ぶ
        /// フェード用のスクリプトがあればフェードして飛ぶ
        /// </summary>
        void ToInGameScene()
        {
            if (Fade.Instance == null)
            {
                Debug.LogWarning("フェード用のスクリプトが見つからなかった");
                SceneManager.LoadScene(_inGameSceneName);
            }
            else
            {
                Fade.Instance.StartFadeOut(() => SceneManager.LoadScene(_inGameSceneName));
            }
        }

        /// <summary>
        /// 図鑑のレイアウト案2種類をボタンクリックで表示
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
