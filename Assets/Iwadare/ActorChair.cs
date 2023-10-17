using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorChair : MonoBehaviour
{
    [Tooltip("ˆÖŽqˆê——")]
    [SerializeField]
    SitScripts[] _chairs;

    public SitScripts ChooseChair(int chooseNum)
    {
        if(chooseNum < _chairs.Length)
        {
            return _chairs[chooseNum];
        }
        return null;
    }
}
