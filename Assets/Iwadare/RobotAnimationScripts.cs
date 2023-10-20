using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotAnimationScripts : MonoBehaviour
{

    bool _sitBool;
    bool _surprisedBool;
    Animator _animator;

    [SerializeField]
    private Transform _custmerTrans;

    [SerializeField]
    Button _sitButton;

    [SerializeField]
    Button _surprisedButton;

    [SerializeField]
    SitScripts _sitScripts;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sitButton.onClick.AddListener(() => SitAnimation());
        _surprisedButton.onClick.AddListener(() => SuprizedAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SitAnimation()
    {
        _sitBool = !_sitBool;
        _animator.SetBool("Sit", _sitBool);
        if(_sitBool == false)
        {
            _custmerTrans.position = _sitScripts.StandUp();
        }
        else
        {
            _custmerTrans.position = _sitScripts.SitDownPosition();
            _custmerTrans.rotation = Quaternion.EulerAngles(_sitScripts.SitDownRotation());
        }
    }

    public void SuprizedAnimation()
    {
        _surprisedBool = !_surprisedBool;
        _animator.SetBool("Surprised", _surprisedBool);
    }


}
