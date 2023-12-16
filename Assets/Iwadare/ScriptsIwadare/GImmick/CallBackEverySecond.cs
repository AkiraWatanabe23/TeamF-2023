using UnityEngine;

public class CallBackEverySecond : MonoBehaviour
{
    [Header("���b���ɃM�~�b�N���쓮�����邩�B")]
    [SerializeField] private float _changeTime = 5f;
    [Header("�M�~�b�N�̃X�N���v�g���A�^�b�`�B")]
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
