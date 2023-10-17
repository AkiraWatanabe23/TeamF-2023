using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proto
{
    public class ActorSit : MonoBehaviour
    {
        SitScripts _sitScripts;
        State _sampleState;
        bool _isSit;
        bool _isLeave;

        // Update is called once per frame
        void Update()
        {
            if(_sampleState == State.Coming) { return; }

            if(_sampleState == State.Eat && !_isSit)
            {
                _isSit = true;
                _sitScripts.SitDownPosition();
                _sitScripts.SitDownRotation();
            }
            else if(_sampleState == State.Leave && !_isLeave)
            {
                _isLeave = true;
                _sitScripts.StandUp();
            }
        }
    }
}
