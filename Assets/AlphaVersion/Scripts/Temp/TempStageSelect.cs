using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Alpha
{
    /// <summary>
    /// ステージセレクト画面の仮のスクリプト
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
        /// ボタンにシーン遷移の処理を登録
        /// </summary>
        void Register()
        {
            foreach (Data data in _data)
            {
                data._button.onClick.AddListener(() => ToStageScene(data._name));
            }
        }

        /// <summary>
        /// フェード用のスクリプトがある場合はフェードインする
        /// </summary>
        void TryFadeIn()
        {
            if (Fade.Instance != null) Fade.Instance.StartFadeIn();
        }

        /// <summary>
        /// 選択したステージに遷移する
        /// </summary>
        void ToStageScene(string sceneName)
        {
            if (Fade.Instance == null)
            {
                Debug.LogWarning("フェード用のスクリプトが見つからなかった");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Fade.Instance.StartFadeOut(() => SceneManager.LoadScene(sceneName));
            }
        }
    }
}
