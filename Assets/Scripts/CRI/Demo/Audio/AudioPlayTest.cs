using UnityEngine;

public class AudioPlayTest : MonoBehaviour
{
    [SerializeField]
    private BGMType _playBGM = BGMType.None;
    [SerializeField]
    private SEType _playSE = SEType.None;

    private SimpleAudioManager _audioManager = default;

    private void Start()
    {
        _audioManager = SimpleAudioManager.Instance.GetInstance();
        _audioManager.PlayBGM(_playBGM);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && _playBGM != BGMType.None) { _audioManager.PlayBGM(_playBGM); }
        if (Input.GetKeyDown(KeyCode.Space) && _playSE != SEType.None) { _audioManager.PlaySE(_playSE); }
    }
}
