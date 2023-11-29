using UnityEngine;

public class BaseGimmickArea : MonoBehaviour
{
    [SerializeField] float _speed;
    public float Speed => _speed;
    [SerializeField] bool _opeStopbool = false;
    public bool OpeStopbool => _opeStopbool;
    public Renderer _opeRenderer;
    [SerializeField] Color _startOpeColor = Color.red;
    public Color StartOpeColor => _startOpeColor;
    [SerializeField] Color _stopOpeColor = Color.white;
    public Color StopOpeColor => _stopOpeColor;
    //[SerializeField] bool _tunbleCallBack = true;

    [SerializeField] float _explosionRadius = 3f;
    public float ExplosionRadius => _explosionRadius;

    private void OnEnable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned += SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea += ChangeAreaOperation;
    }

    private void OnDisable()
    {
        //if (_tunbleCallBack) { TumbleweedSpawner.OnSpawned -= SpeedChangeAreaOperation; }
        ChangeAreaCallBackTest.OnCallBackArea -= ChangeAreaOperation;
    }

    void Start()
    {
        ChangeAreaOperation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_explosionRadius);
    }

    public void ChangeAreaOperation()
    {
        if (_opeStopbool == true)
        {
            _opeStopbool = false;
            _opeRenderer.material.color = _stopOpeColor;
        }
        else
        {
            _opeStopbool = true;
            _opeRenderer.material.color = _startOpeColor;
        }
    }
}