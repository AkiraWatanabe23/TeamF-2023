using UnityEngine;

public class BaseGimmickArea : MonoBehaviour
{
    [SerializeField] float _speed;
    public float Speed => _speed;
    [SerializeField] bool _opeStopbool = false;
    public bool OpeStopbool => _opeStopbool;
    [SerializeField] Renderer _mesh;
    [SerializeField] Color _startOpeColor = Color.red;
    [SerializeField] Color _stopOpeColor = Color.white;
    //[SerializeField] bool _tunbleCallBack = true;

    private void OnEnable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned += SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea += SpeedChangeAreaOperation;
    }

    private void OnDisable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned -= SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea -= SpeedChangeAreaOperation;
    }

    void Start()
    {
        SpeedChangeAreaOperation();
    }

    void SpeedChangeAreaOperation()
    {
        if (_opeStopbool == true)
        {
            _opeStopbool = false;
            _mesh.material.color = _stopOpeColor;
        }
        else
        {
            _opeStopbool = true;
            _mesh.material.color = _startOpeColor;
        }
    }
}