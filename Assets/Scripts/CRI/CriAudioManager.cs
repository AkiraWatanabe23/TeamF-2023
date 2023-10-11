using CriWare;

/// <summary> CRIを用いたAudioManager </summary>
public class CriAudioManager
{
    private static CriAudioManager _instance = default;

    //音源再生用のクラス
    private readonly CriAtomExPlayer _criBGMPlayer = new();
    private readonly CriAtomExPlayer _criSEPlayer = new();


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

    }

    public void PlayBGM(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        _criBGMPlayer.SetCue(acb, cueName);

        _criBGMPlayer.Start();
    }

    public void PlaySE(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        _criSEPlayer.SetCue(acb, cueName);

        _criSEPlayer.Start();
    }

    public void StopBGM() { }

    public void StopSE() { }
}
