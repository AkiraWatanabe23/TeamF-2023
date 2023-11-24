using UnityEngine;

public class SitRequest : MonoBehaviour
{
    [SerializeField] SitScripts[] _sitScripts;

    public SitScripts[] SitScriptsRequest()
    {
        return _sitScripts;
    }
}
