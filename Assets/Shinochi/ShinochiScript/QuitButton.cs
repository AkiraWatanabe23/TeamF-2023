using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // �{�^���̎Q�Ƃ�ێ����܂�
    [SerializeField]private Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        // �Q�[�����I��
        Application.Quit();
        //�X�e�[�W���Z�b�g
        PlayerPrefs.DeleteAll();
        //�I���e�X�g
        Debug.Log("�Q�[���I��");
    }
}
