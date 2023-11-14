using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweeds : MonoBehaviour
{
    [SerializeField, Header("“–‚½‚è”»’è‚Ì‚½‚ß‚ÌTag")]private string _collisionTag;

    [SerializeField, Header("Á‚¦‚é‚Ü‚Å‚ÌŽžŠÔ")] private float _timeLimit;

    private float _currntTime = 0f; //Œ»Ý‚ÌŽžŠÔ

    private bool _timeBool = false; //ŽžŠÔŒv‘ª‚Ì‚½‚ß


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
