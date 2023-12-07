using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneChenger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void TestChenger()
    {
        SceneManager.LoadScene(sceneName); // ƒV[ƒ“‘JˆÚ
    }
}
