using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScripts : MonoBehaviour
{
    bool _sitBool;
    Animator _animator;

    [SerializeField]
    Button _sitButton;

    [SerializeField]
    Text _sitText;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sitButton.onClick.AddListener(() => SitAnimation(" Button Clicked"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SitAnimation(string text)
    {
        _sitBool = !_sitBool;

        _sitText.text = text;

        if (_sitBool)
        {
            _animator.Play("Sitting");
        }
    }

}
