using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweeds : MonoBehaviour
{
    [SerializeField, Header("�����蔻��̂��߂�Tag")]private string _collisionTag;

    [SerializeField, Header("������܂ł̎���")] private float _timeLimit;

    private float _currntTime = 0f; //���݂̎���

    private bool _timeBool = false; //���Ԍv���̂���


    private void Update()
    {
        if(_timeBool)
        {
            _currntTime += Time.deltaTime;

            if(_currntTime >= _timeLimit)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        _timeBool = false;
        _currntTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _collisionTag)
        {
            _timeBool = true;
        }
    }
}
