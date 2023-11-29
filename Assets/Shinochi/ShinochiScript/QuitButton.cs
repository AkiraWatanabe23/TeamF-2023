using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // ボタンの参照を保持します
    [SerializeField]private Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        // ゲームを終了
        Application.Quit();
        //ステージリセット
        PlayerPrefs.DeleteAll();
        //終了テスト
        Debug.Log("ゲーム終了");
    }
}
