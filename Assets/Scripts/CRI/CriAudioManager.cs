using System;
using CriWare;

/// <summary> CRIを用いたAudioManager </summary>
public class CriAudioManager
{
    private static CriAudioManager _instance = default;

    //CriAtomExPlayer：音源再生用のクラス
    private readonly CriAtomExPlayer _criBGMPlayer = new();
    private readonly CriAtomExPlayer _criSEPlayer = new();

    private readonly CriAtomExPlayback _bgmPlayback = default;

    private float _bgmVolume = 1f;
    private float _seVolume = 1f;

    //音量変更時に発行されるイベント
    public event Action<float> OnValueChangedBGMVolume;
    public event Action<float> OnValueChangedSEVolume;

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


    public static CriAudioManager Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private CriAudioManager()
    {
        //コンストラクタ
        OnValueChangedBGMVolume += value =>
        {
            _criBGMPlayer.SetVolume(value * _bgmVolume);
            _criBGMPlayer.Update(_bgmPlayback);
        };

        OnValueChangedSEVolume += value =>
        {
            _criSEPlayer.SetVolume(value * _bgmVolume);
        };
    }

    ~CriAudioManager() { }

    public void PlayBGM(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        _criBGMPlayer.SetCue(acb, cueName);

        _criBGMPlayer.Start();
    }

    public void PauseBGM() { _criBGMPlayer.Pause(); }

    public void ResumrBGM() { }

    public void StopBGM() { }

    public void PlaySE(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        _criSEPlayer.SetCue(acb, cueName);

        _criSEPlayer.Start();
    }

    public void PauseSE() { }

    public void ResumeSE() { }

    public void StopSE() { }
}
