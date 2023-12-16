using System.Collections;
using UnityEngine;

public class CallBackSpecifiedSecond : MonoBehaviour
{
    [Header("開始から何秒でギミックを作動させるか(配列0から小さい順で)")]
    [SerializeField] float[] _gimmickOperationTime;
    [Header("ギミックのスクリプトをアタッチ。")]
    [SerializeField] BaseGimmickArea _gimmickArea;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GimmickCallBackTime());
    }

    /// <summary>指定時間にギミックを作動させるコルーチン。</summary>
    /// <returns></returns>
    IEnumerator GimmickCallBackTime()
    {
        var time = 0f;
        for(var i = 0;i < _gimmickOperationTime.Length;i++)
        {
            for(;time < _gimmickOperationTime[i];time += Time.deltaTime)
            {
                yield return null;
            }
            _gimmickArea.GimmickAction.Invoke();
            yield return null;
        }
    }
}
