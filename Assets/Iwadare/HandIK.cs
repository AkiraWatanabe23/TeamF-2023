using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIK : MonoBehaviour
{
    [SerializeField]
    Transform _glassTarget;

    [SerializeField, Range(0, 1)]
    float _rightHandPosition;

    [SerializeField, Range(0, 1)]
    float _rightHandRotation;

    [SerializeField, Range(0, 1)]
    float _leftHandPosition;

    [SerializeField, Range(0, 1)]
    float _leftHandRotation;

    [SerializeField]
    bool _ikBool;

    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnAnimatorIK(int layerIndex)
    {
        if (_glassTarget && _ikBool)
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandPosition);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightHandRotation);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, _glassTarget.position);
            _anim.SetIKRotation(AvatarIKGoal.RightHand, _glassTarget.rotation);
        }
    }
}
