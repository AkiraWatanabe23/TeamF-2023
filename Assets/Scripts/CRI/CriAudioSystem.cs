using CriWare;

public class CriAudioSystem
{
    private readonly CriAudioData _audioData = new();

    public void OnValueChangedSetting()
    {
        _audioData.OnValueChangedBGMVolume += value =>
        {
            _audioData.CriBGMPlayer.SetVolume(value * _audioData.BGMVolume);
            _audioData.CriBGMPlayer.Update(_audioData.BgmPlayback);
        };

        _audioData.OnValueChangedSEVolume += value =>
        {
            _audioData.CriSEPlayer.SetVolume(value * _audioData.SEVolume);
        };
    }

    //=============== BGM ===============
    #region BGM
    public void PlayBGM(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        //現在再生中のBGMを停止させる
        StopBGM();

        _audioData.CriBGMPlayer.SetCue(acb, cueName);
        _audioData.CriBGMPlayer.SetVolume(_audioData.BGMVolume);

        _audioData.CriBGMPlayer.Start();
    }

    public void PauseBGM() { _audioData.CriBGMPlayer.Pause(); }

    public void ResumeBGM()
    {
        //Pause()によって停止したBGMを再生する
        _audioData.CriBGMPlayer.Resume(CriAtomEx.ResumeMode.PausedPlayback);
    }

    public void StopBGM() { _audioData.CriBGMPlayer.Stop(); }
    #endregion

    //=============== SE ===============
    #region SE
    public void PlaySE(string cueSheetName, string cueName)
    {
        //どの音を再生するか選択する
        var acb = CriAtom.GetCueSheet(cueSheetName).acb;
        _audioData.CriSEPlayer.SetCue(acb, cueName);
        _audioData.CriSEPlayer.SetVolume(_audioData.SEVolume);

        //listに追加する

        _audioData.CriSEPlayer.Start();
    }

    public void PauseSE(int index)
    {
        _audioData.SEData[index].Playback.Pause();
    }

    public void ResumeSE(int index)
    {
        _audioData.SEData[index].Playback.Resume(CriAtomEx.ResumeMode.PausedPlayback);
    }

    public void StopSE(int index)
    {
        _audioData.SEData[index].Playback.Stop();
    }

    public void StopAllSE()
    {
        foreach (var se in _audioData.SEData) { se.Playback.Stop(); }
    }
    #endregion
}
