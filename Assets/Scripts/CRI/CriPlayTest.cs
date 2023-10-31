using UnityEngine;
using UnityEngine.UI;

public class CriPlayTest : MonoBehaviour
{
    [SerializeField]
    private Button _playButton = default;
    [SerializeField]
    private string _cueName = "";

    private void Start()
    {
        _playButton.onClick.AddListener(() => CriAudioManager.Instance.SE.Play("CueSheet_SE", _cueName));
    }
}
