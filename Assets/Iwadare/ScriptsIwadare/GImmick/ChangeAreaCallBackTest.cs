using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeAreaCallBackTest : MonoBehaviour
{
    public static event UnityAction OnCallBackArea;
    [SerializeField] private float _changeTime = 5f;
    float _time;

    private void OnDestroy()
    {
        OnCallBackArea = null;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time > _changeTime)
        {
            OnCallBackArea?.Invoke();
            _time = 0;
        }
    }
}
