using Alpha;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ItemSpeedChangeArea : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] bool _opeStopbool = false;
    [SerializeField] Renderer _mesh;
    [SerializeField] Color _startOpeColor = Color.red;
    [SerializeField] Color _stopOpeColor = Color.white;
    [SerializeField] bool _tunbleCallBack = true;

    private void OnEnable()
    {
        if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned += SpeedChangeAreaOperation; }
        else { ChangeAreaCallBackTest.OnCallBackArea += SpeedChangeAreaOperation; }
    }

    private void OnDisable()
    {
        if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned -= SpeedChangeAreaOperation; }
        else { ChangeAreaCallBackTest.OnCallBackArea -= SpeedChangeAreaOperation; }
    }

    void Start()
    {
        SpeedChangeAreaOperation();
    }

    void SpeedChangeAreaOperation()
    {
        if(_opeStopbool == true)
        {
            _opeStopbool = false;
            _mesh.material.color = _stopOpeColor;
        }
        else
        {
            _opeStopbool = true;
            _mesh.material.color = _startOpeColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_opeStopbool == true) 
        {
            var rb = other.GetComponent<Rigidbody>();
            var velocity = rb.velocity;
            velocity.x = rb.velocity.x * _speed;
            velocity.z = rb.velocity.z * _speed;
            if (_speed != 0)
            {
                rb.AddForce(velocity, ForceMode.Impulse);
            }
            else
            {
                rb.velocity = velocity;
            }
        }
    }
}
