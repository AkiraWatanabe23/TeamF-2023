using System.Collections;
using UnityEngine;

public class CallBackSpecifiedSecond : MonoBehaviour
{
    [Header("�J�n���牽�b�ŃM�~�b�N���쓮�����邩(�z��0���珬��������)")]
    [SerializeField] float[] _gimmickOperationTime;
    [Header("�M�~�b�N�̃X�N���v�g���A�^�b�`�B")]
    [SerializeField] BaseGimmickArea _gimmickArea;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GimmickCallBackTime());
    }

    /// <summary>�w�莞�ԂɃM�~�b�N���쓮������R���[�`���B</summary>
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
