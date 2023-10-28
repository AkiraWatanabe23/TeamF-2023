using CriWare;
using System;
using System.Collections.Generic;

public class CriAudioData
{
    public struct CriSEData
    {
        public CriAtomExPlayback Playback { get; private set; }
        public CriAtomEx.CueInfo CueInfo { get; private set; }
    }

    //CriAtomExPlayer：音源再生用のクラス
    private readonly CriAtomExPlayer _criBGMPlayer = new();
    private readonly CriAtomExPlayer _criSEPlayer = new();

    private readonly CriAtomExPlayback _bgmPlayback = default;

    private float _bgmVolume = 1f;
    private float _seVolume = 1f;

    private readonly List<CriSEData> _seData = new();

    //音量変更時に発行されるイベント
    public event Action<float> OnValueChangedBGMVolume;
    public event Action<float> OnValueChangedSEVolume;

    public CriAtomExPlayer CriBGMPlayer => _criBGMPlayer;
    public CriAtomExPlayer CriSEPlayer => _criSEPlayer;
    public CriAtomExPlayback BgmPlayback => _bgmPlayback;

    public float BGMVolume
    {
        get => _bgmVolume;
        set
        {
            _bgmVolume = value;
            OnValueChangedBGMVolume?.Invoke(value);
        }
    }
    public float SEVolume
    {
        get => _seVolume;
        set
        {
            _seVolume = value;
            OnValueChangedSEVolume?.Invoke(value);
        }
    }
    public List<CriSEData> SEData => _seData;
}
