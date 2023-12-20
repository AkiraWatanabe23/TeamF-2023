using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DemoPlayScripts : MonoBehaviour
{
    VideoPlayer _video;
    [SerializeField] RawImage _image;
    [Header("デモプレイが流れるまでの放置時間")]
    [SerializeField] float _demoPlayWaitTime = 3f;
    [Header("流すビデオをアタッチ(数種類の場合は0から順番にひとつずつ流れる)")]
    [SerializeField] VideoClip[] _clips;
    int index = 0;
    float _time = 0;
    [SerializeField] bool _awakePlaying = false;
    [SerializeField] bool _isBoth = false;
    [SerializeField] Color _stopColor = Color.clear;
    VideoPlaying _videoState = VideoPlaying.Stopping;

    private void Awake()
    {
        _video = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        if (_awakePlaying)
        {
            _video.clip = _clips[index];
            _video.Play();
            index = index + 1;
            _videoState = VideoPlaying.Playing;
        }
        else
        {
            _image.color = _stopColor;
            _image.raycastTarget = false;
        }
    }


    void Update()
    {
        if (_videoState == VideoPlaying.Stopping)
        {
            if (!Input.anyKeyDown)
            {
                _time += Time.deltaTime;
            }
            else
            {
                _time = 0;
            }

            if (_time > _demoPlayWaitTime)
            {
                StartPlayer();
                Debug.Log("Start！");
            }
        }
        else
        {
            if(_video.isPaused)
            {
                StopPlayer();
                Debug.Log("Stop!");
            }

            if (Input.anyKeyDown)
            {
                StopPlayer();
                Debug.Log("Stop!b");
            }
        }
    }

    private void StartPlayer()
    {
        _time = 0;
        _image.DOFade(1f, 1f).SetLink(gameObject);
        _image.raycastTarget = true;
        _video.clip = _clips[index];
        _video.Play();
        index = index + 1;
        _videoState = VideoPlaying.Playing;
    }

    private void StopPlayer()
    {
        _video.Stop();
        index = (index + _clips.Length) % _clips.Length;
        _image.DOFade(0f, 1f).SetLink(gameObject);
        _image.raycastTarget = false;
        _videoState = VideoPlaying.Stopping;
    }

    enum VideoPlaying
    {
        Stopping,
        Playing,
    }
}
