using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLoadButton : MonoBehaviour
{
    //[SerializeField]
    //private SceneName _loadScene = SceneName.Title;

    public void OnClickFade()
    {
        //Fade.Instance.RegisterFadeOutEvent(new Action[] { () => SceneManager.LoadScene(Consts.Scenes[_loadScene]) });
        Fade.Instance.StartFadeOut();
    }
}

public static class Consts
{
    private static readonly Dictionary<SceneName, string> _scenes = new()
    {
        { SceneName.Title, "TitleScene" },
        { SceneName.StageSelect, "StageSelectScene" },
        { SceneName.InGame, "MasterGameScene" },
        { SceneName.TestTitle, "TestTitle" },
        { SceneName.Test1, "TestStage1" },
        { SceneName.Test2, "TestStage2" },
        { SceneName.Test3, "TestStage3" },
    };

    public static Dictionary<SceneName, string> Scenes => _scenes;
}

public enum SceneName : int
{
    Title,
    StageSelect,
    InGame,
    TestTitle,
    Test1,
    Test2,
    Test3,
}
