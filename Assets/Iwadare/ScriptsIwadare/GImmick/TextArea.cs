using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextArea : BaseGimmickArea
{
    Vector3 _initPos;
    [SerializeField]Rigidbody _rb;
    [SerializeField] Vector3 _moveVec = Vector3.zero;
    [SerializeField] float _activeTime = 5f;
    [SerializeField] float _power = 5f;

    void Start()
    {
        _initPos = _rb.transform.position;
    }


    public override void GimmickOperation()
    {
        _rb.velocity = _moveVec.normalized * Speed;
        StartCoroutine(GimmickMove());
    }

    public IEnumerator GimmickMove()
    {
        for(var time = 0f;time < _activeTime;time += Time.deltaTime){ yield return null; }
        _rb.transform.position = _initPos;
        _rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        var objRb = other.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.AddForce(_moveVec.normalized * _power,ForceMode.Impulse);
        }
    }
}
