using UnityEngine;

public class CallBackEverySecond : MonoBehaviour
{
    [Header("何秒毎にギミックを作動させるか。")]
    [SerializeField] private float _changeTime = 5f;
    [Header("ギミックのスクリプトをアタッチ。")]
    [SerializeField] BaseGimmickArea _gimmickArea;
    float _time;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _changeTime)
        {
            _gimmickArea?.GimmickAction.Invoke();
            _time = 0;
        }
    }
}
