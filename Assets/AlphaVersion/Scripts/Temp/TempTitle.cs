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

        void Awake()
        {
            _playButton.onClick.AddListener(ToInGameScene);
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
    }
}
