using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectArea : BaseGimmickArea
{
    [SerializeField] 
    bool _reverse;
    private void OnCollisionEnter(Collision collision)
    {
        var objRb = collision.gameObject.GetComponent<Rigidbody>();
        var normalVec =  _reverse ? transform.position - Vector3.forward : transform.position + Vector3.forward;
        if(objRb != null) 
        {
            objRb.AddForce(Vector3.Reflect(objRb.velocity, Vector3.Cross(Vector3.up,normalVec)) * Speed,ForceMode.Impulse);
        }
    }
}
