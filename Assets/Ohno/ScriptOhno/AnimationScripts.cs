using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScripts : MonoBehaviour
{
    bool _walkBool;
    bool _sitBool;
    bool _successBool;
    bool _failedBool;
    bool _danceBool;
    Animator _animator;

    [SerializeField]
    Button _walkButton;
    [SerializeField]
    Button _sitButton;
    [SerializeField]
    Button _successButton;
    [SerializeField]
    Button _failedButton;
    [SerializeField]
    Button _danceButton;

    [SerializeField]
    Text _text;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _walkButton.onClick.AddListener(() => WalkAnimation("WalkButton Clicked"));
        _sitButton.onClick.AddListener(() => SitAnimation("SitButton Clicked"));
        _successButton.onClick.AddListener(() => SuccessAnimation("SuccessButton Clicked"));
        _failedButton.onClick.AddListener(() => FailedAnimation("FailedButton Clicked"));
        _danceButton.onClick.AddListener(() => DanceAnimation("DanceButton Clicked"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WalkAnimation(string text)
    {
        _walkBool = !_walkBool;

        if (_walkBool)
        {
            _animator.Play("Walk");
            _text.text = text;
        }
    }

    public void SitAnimation(string text)
    {
        _sitBool = !_sitBool;

        if (_sitBool)
        {
            _animator.Play("Sitting");
            _text.text = text;
        }
    }

    public void SuccessAnimation(string text)
    {
        _successBool = !_successBool;

        if (_successBool)
        {
            _animator.Play("Surprized");
            _text.text = text;
        }
    }

    public void FailedAnimation(string text)
    {
        _failedBool = !_failedBool;

        if (_failedBool)
        {
            _animator.Play("Failed");
            _text.text = text;
        }
    }

    public void DanceAnimation(string text)
    {
        _danceBool = !_danceBool;

        if (_danceBool)
        {
            _animator.Play("Dance");
            _text.text = text;
        }
    }

}
