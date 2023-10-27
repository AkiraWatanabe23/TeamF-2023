/// <summary> CRIを用いたAudioManager </summary>
public class CriAudioMain
{
    private readonly CriAudioSystem _audioSystem = default;

    private static CriAudioMain _instance = default;

    public static CriAudioMain Instance
    {
        get
        {
            _instance ??= new CriAudioMain();
            return _instance;
        }
    }

    private CriAudioMain()
    {
        _audioSystem ??= new CriAudioSystem();
        _audioSystem.OnValueChangedSetting();
    }

    ~CriAudioMain()
    {
        //登録した関数の破棄とか
    }

    //=============== BGM ===============
    #region BGM
    public void PlayBGM(string cueSheetName, string cueName)
    {
        _audioSystem.PlayBGM(cueSheetName, cueName);
    }

    public void PauseBGM() { _audioSystem.PauseBGM(); }

    public void ResumeBGM()
    {
        //Pause()によって停止したBGMを再生する
        _audioSystem.ResumeBGM();
    }

    public void StopBGM() { _audioSystem.StopBGM(); }
    #endregion

    //=============== SE ===============
    #region SE
    public void PlaySE(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        _audioSystem.PlaySE(cueSheetName, cueName);
    }

    public void PauseSE(int index)
    {
        if (index < 0) { return; }
        _audioSystem.PauseSE(index);
    }

    public void ResumeSE(int index)
    {
        if (index < 0) { return; }
        _audioSystem.ResumeSE(index);
    }

    public void StopSE(int index)
    {
        _audioSystem.StopSE(index);
    }

    public void StopAllSE()
    {
        _audioSystem.StopAllSE();
    }
    #endregion

    public void Play3D() { }
}
