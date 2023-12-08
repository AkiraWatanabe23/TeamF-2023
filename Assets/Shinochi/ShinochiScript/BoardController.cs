using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float force = 10.0f;
    void Start()
    {
        // Rigidbody�R���|�[�l���g��ǉ�
        rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.down * force, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * force, ForceMode.Force);
    }
}
