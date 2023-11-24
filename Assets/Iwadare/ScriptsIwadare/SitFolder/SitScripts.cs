using System;
using UnityEngine;

public class SitScripts : MonoBehaviour
{
    [SerializeField]
    Trans _sitDown;
    [SerializeField]
    Trans _standUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + _sitDown.Position, 0.05f);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + _standUp.Position, new Vector3(0.1f, 0.1f, 0.1f));
    }


    public Vector3 SitDownPosition()
    {
        return transform.position + _sitDown.Position;
    }

    public Vector3 SitDownRotation()
    {
        return _sitDown.Rotation;
    }

    public Vector3 StandUp()
    {
        return transform.position + _standUp.Position;
    }

    public Vector3 StandUpRotation()
    {
        return _standUp.Rotation;
    }

    [Serializable]
    public struct Trans
    {
        public Vector3 Position;
        public Vector3 Rotation;
    }
}
