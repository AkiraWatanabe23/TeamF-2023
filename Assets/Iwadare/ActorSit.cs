using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proto
{
    public class ActorSit : MonoBehaviour
    {
        SitScripts _sitScripts;
        Transform _custmerTrans;
        //State _sampleState;
        bool _isSit;
        bool _isLeave;


        void Update()
        {
            //    if(_sampleState == State.Coming) { return; }

            //    if(_sampleState == State.Eat && !_isSit)
            //    {
            //        _isSit = true;
            //        _custmerTrans.position = _sitScripts.SitDownPosition();
            //        _custmerTrans.rotation = Quaternion.EulerAngles(_sitScripts.SitDownRotation());
            //    }
            //    else if(_sampleState == State.Leave && !_isLeave)
            //    {
            //        _isLeave = true;
            //        _sitScripts.StandUp();
            //    }
        }
    }
}
