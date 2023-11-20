using UnityEngine;

public class WindArea : BaseGimmickArea
{
    private void OnTriggerStay(Collider other)
    {
        if (OpeStopbool == true)
        {
            var rb = other.GetComponent<Rigidbody>();
            var velocity = rb.velocity;
            velocity = Vector3.forward * Speed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
    }
}
