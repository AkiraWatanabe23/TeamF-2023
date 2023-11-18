using UnityEngine;
using UnityEngine.UI;

public class CriPlayTest : MonoBehaviour
{
    [SerializeField]
    private Button _playButton = default;
    [SerializeField]
    private Button _pauseButton = default;
    [SerializeField]
    private string _cueSheetName = "CueSheet_SE";
    [SerializeField]
    private string _cueName = "";

    public string CueSheetName => _cueSheetName;

    private void Start()
    {
        _playButton.onClick.AddListener(() => CriAudioManager.Instance.BGM.Play(_cueSheetName, _cueName));
        _pauseButton.onClick.AddListener(() => CriAudioManager.Instance.BGM.StopAll());
    }
}
