using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DemoPlayScripts : MonoBehaviour
{
    VideoPlayer _video;
    [SerializeField] RawImage _image;
    [Header("�f���v���C�������܂ł̕��u����")]
    [SerializeField] float _demoPlayWaitTime = 3f;
    [Header("�����r�f�I���A�^�b�`(����ނ̏ꍇ��0���珇�ԂɂЂƂ������)")]
    [SerializeField] VideoClip[] _clips;
    int _index = 0;
    float _time = 0;
    [SerializeField] bool _awakePlaying = false;
    bool _isMovieEnd;
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
            _video.clip = _clips[_index];
            _video.Play();
            _index = _index + 1;
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
                VideoSetUp();
                StartPlayer();
                Debug.Log("Start�I");
            }
        }
        else
        {
            if(_video.isPaused && !_isMovieEnd)
            {
                _isMovieEnd = true;
                ChangePlayer();
                Debug.Log("����؂�ւ�");
            }

            if (Input.anyKeyDown)
            {
                StopPlayer();
                Debug.Log("Stop!button");
            }
        }
    }

    /// <summary>�r�f�I�Đ�����ۂ̃Z�b�g�A�b�v</summary>
    void VideoSetUp()
    {
        _time = 0;
        _index = 0;
        _image.DOFade(1f, 1f).SetLink(gameObject);
        _image.raycastTarget = true;
        _videoState = VideoPlaying.Playing;
    }

    /// <summary>����؂�ւ����čĐ�</summary>
    void ChangePlayer()
    {
        _video.Stop();
        _index = (_index + _clips.Length) % _clips.Length;
        Debug.Log(_index);
        var changeSeq = DOTween.Sequence();
        changeSeq.Append(_image.DOColor(Color.black, 0.5f))
            .AppendCallback(() => { StartPlayer(); })
            .Append(_image.DOColor(Color.white, 0.5f))
            .OnUpdate(() => { if (Input.anyKeyDown) { changeSeq.Complete(); } })
            .OnComplete(() => { _isMovieEnd = false; });
        changeSeq.Play().SetLink(gameObject);
    }

    /// <summary>�r�f�I�Đ�</summary>
    private void StartPlayer()
    {
        Debug.Log(_index);
        _video.clip = _clips[_index];
        _video.Play();
        _index = _index + 1;
    }

    /// <summary>�r�f�I��~</summary>
    private void StopPlayer()
    {
        _video.Stop();
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
