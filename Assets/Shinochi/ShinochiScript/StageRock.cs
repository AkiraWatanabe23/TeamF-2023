using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRock : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons;

    void Start()
    {
        // ステージ１は最初からアンロック
        PlayerPrefs.SetInt("stage1", 1);

        for (int i = 0; i < stageButtons.Length; i++)
        {
            int level = i + 1;
            if (PlayerPrefs.GetInt("stage" + level.ToString()) == 1)
            {
                stageButtons[i].interactable = true;
            }
            else
            {
                stageButtons[i].interactable = false;
            }
        }
    }

    public void UnlockStage(int stage)
    {
        if (stage <= stageButtons.Length)
        {
            PlayerPrefs.SetInt("stage" + stage.ToString(), 1);
            stageButtons[stage - 1].interactable = true;
        }
    }
}
