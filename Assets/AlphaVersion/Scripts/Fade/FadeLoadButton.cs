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
    };

    public static Dictionary<SceneName, string> Scenes => _scenes;
}

public enum SceneName : int
{
    Title,
    StageSelect,
    InGame,
}
