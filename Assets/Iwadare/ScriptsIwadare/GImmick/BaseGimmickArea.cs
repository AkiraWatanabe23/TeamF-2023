using System;
using UnityEngine;

public class BaseGimmickArea : MonoBehaviour
{
    [Header("速度を変更させるギミックの速度")]
    [SerializeField] float _speed;
    public float Speed => _speed;
    [SerializeField] bool _gimmickOperationBool = false;
    public bool GimmickOperationBool => _gimmickOperationBool;
    public Renderer _gimmickOpeRenderer;
    [SerializeField] Color _startGimmickOpeColor = Color.red;

    public Color StartGimmickOpeColor => _startGimmickOpeColor;
    [SerializeField] Color _stopGimmickOpeColor = Color.white;
    public Color StopGimmickOpeColor => _stopGimmickOpeColor;
    //[SerializeField] bool _tunbleCallBack = true;

    [SerializeField] float _explosionRadius = 3f;
    public float ExplosionRadius => _explosionRadius;

    public virtual void GimmickOperation() { }

    private void OnEnable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned += SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea += ChangeAreaOperation;
        ChangeAreaCallBackTest.OnCallBackArea += GimmickOperation;
    }

    private void OnDisable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned -= SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea -= ChangeAreaOperation;
        ChangeAreaCallBackTest.OnCallBackArea -= GimmickOperation;
    }

    void Start()
    {
        if(_gimmickOperationBool == false)
        {
            _gimmickOpeRenderer.material.color = _stopGimmickOpeColor;
        }
        else
        {
            _gimmickOpeRenderer.material.color = _startGimmickOpeColor;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_explosionRadius);
    }

    public void ChangeAreaOperation()
    {
        if (_gimmickOperationBool == true)
        {
            _gimmickOperationBool = false;
            _gimmickOpeRenderer.material.color = _stopGimmickOpeColor;
        }
        else
        {
            _gimmickOperationBool = true;
            _gimmickOpeRenderer.material.color = _startGimmickOpeColor;
        }
    }
}