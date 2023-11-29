using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpArea : BaseGimmickArea
{
    [SerializeField] Transform _warpEndArea;
    private void OnTriggerEnter(Collider other)
    {
        if(GimmickOperationBool)
        {
            var tmpVec = other.transform.position;
            tmpVec.x = _warpEndArea.position.x;
            tmpVec.z = _warpEndArea.position.z;
            other.transform.position = tmpVec;
        }
    }
}
