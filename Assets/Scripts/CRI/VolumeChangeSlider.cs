using UnityEngine;
using UnityEngine.UI;

public class VolumeChangeSlider : MonoBehaviour
{
    [SerializeField]
    private SliderType _volumeType = SliderType.Master;

    private Slider _volumeSlider = default;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        TryGetComponent(out _volumeSlider);
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        _volumeSlider.value = 1f;
    }

    private void OnVolumeChanged(float value)
    {
        switch (_volumeType)
        {
            case SliderType.Master:
                CriAudioManager.Instance.MasterVolume.Value = value; break;
            case SliderType.BGM:
                CriAudioManager.Instance.BGM.Volume.Value = value; break;
            case SliderType.SE:
                CriAudioManager.Instance.SE.Volume.Value = value; break;
        }
    }
}

public enum SliderType
{
    Master,
    BGM,
    SE,
}
