using UnityEngine;

public class DebugLogUtility : MonoBehaviour
{
    public static void PrankLog(string log, bool viewFlag = false)
    {
        if (viewFlag) { Debug.Log(log); }
    }
}
