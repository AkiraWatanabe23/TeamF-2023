using UnityEngine;

public class ItemSpeedChangeArea : BaseGimmickArea
{


    private void OnTriggerEnter(Collider other)
    {
        if (OpeStopbool == true)
        {
            var rb = other.GetComponent<Rigidbody>();
            var velocity = rb.velocity;
            velocity.x = rb.velocity.x * Speed;
            velocity.z = rb.velocity.z * Speed;
            if (Speed != 0)
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
