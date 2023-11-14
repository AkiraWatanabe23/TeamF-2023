using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Alpha
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] CinemachineImpulseSource _source;

        CinemachineVirtualCamera _vCam;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // “r’†
                _source.GenerateImpulseAt(Vector3.zero, new Vector3(0.1f, 0.1f, 0));
            }
        }
    }
}