using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitScripts : MonoBehaviour
{
    [SerializeField]
    Transform _sitDownTrans;

    [SerializeField]
    Transform _standUpTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 SitDownPosition()
    {
        return _sitDownTrans.position;
    }

    public Vector3 SitDownRotation()
    {
        Vector3 vec = transform.localEulerAngles;
        return vec;
    }

    public Vector3 StandUp()
    {
        return _standUpTrans.position;
    }
}
