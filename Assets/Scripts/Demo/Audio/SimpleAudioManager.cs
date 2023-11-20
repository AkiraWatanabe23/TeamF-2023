using UnityEngine;

/// <summary> 簡易的なAudioManager（AudioSource） </summary>
public class SimpleAudioManager
{
    private static AudioSource _bgmSource = default;
    private static AudioSource _seSource = default;

    private static AudioHolder _audioHolder = default;
    private static SimpleAudioManager _instance = default;

    private static float _bgmVolume = 1f;
    private static float _seVolume = 1f;

    public AudioSource BGMSource => _bgmSource;
    public AudioSource SeSource => _seSource;

    public static SimpleAudioManager Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    /// <summary> AudioManagerの初期化処理 </summary>
    private static void Init()
    {
        if (_instance == null)
        {
            var sound = new GameObject("AudioManager");
            _instance = new();

            var bgm = new GameObject("BGM");
            _bgmSource = bgm.AddComponent<AudioSource>();
            bgm.transform.parent = sound.transform;

            var se = new GameObject("SE");
            _seSource = se.AddComponent<AudioSource>();
            se.transform.parent = sound.transform;

            _audioHolder = Resources.Load<AudioHolder>("AudioHolder");

            //音量設定
            _bgmSource.volume = _bgmVolume;
            _seSource.volume = _seVolume;

            Object.DontDestroyOnLoad(sound);
        }
    }

    public SimpleAudioManager GetInstance() { return _instance; }

    /// <summary> BGM再生 </summary>
    /// <param name="bgm"> どのBGMか </param>
    /// <param name="isLoop"> ループ再生するか </param>
    public void PlayBGM(BGMType bgm, bool isLoop = true)
    {
        var index = 0;
        foreach (var clip in _audioHolder.BGMClips)
        {
            if (clip.BGMType == bgm) { break; }

            index++;
        }

        _bgmSource.Stop();

        _bgmSource.loop = isLoop;
        _bgmSource.clip = _audioHolder.BGMClips[index].BGMClip;
        _bgmSource.Play();
    }

    /// <summary> SE再生 </summary>
    /// <param name="se"> どのSEか </param>
    public void PlaySE(SEType se)
    {
        var index = 0;
        foreach (var clip in _audioHolder.SEClips)
        {
            if (clip.SEType == se) { break; }

            index++;
        }
        _seSource.PlayOneShot(_audioHolder.SEClips[index].SEClip);
    }

    /// <summary> BGMの再生を止める </summary>
    public void StopBGM() { _bgmSource.Stop(); }

    /// <summary> SEの再生を止める </summary>
    public void StopSE() { _seSource.Stop(); }
}

public enum BGMType
{
    None,
    Title,
    StageSelect,
    InGame,
    Result,
}

public enum SEType
{
    None,
    Throw,
}
