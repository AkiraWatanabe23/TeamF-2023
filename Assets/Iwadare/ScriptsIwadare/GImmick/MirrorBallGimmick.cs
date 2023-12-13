using DG.Tweening;
using UnityEngine;

public class MirrorBallGimmick : BaseGimmickArea
{
    [SerializeField] Transform _mirrorBallObj;
    [SerializeField] float _fallTransY = 0.8f;
    [SerializeField] float _upTransY = 3f;
    [SerializeField] float _allGimmickTime = 4f;
    [SerializeField] float _expPower = 3f;
    [SerializeField] float _expRadius = 3f;
    Vector3 _gizmoSize = new Vector3(0.3f, 0.3f, 0.3f);
    bool _mirrorBallrota = false;

    private void Start()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, _upTransY, transform.position.z), _gizmoSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, _fallTransY, transform.position.z), _gizmoSize);
    }

    public override void GimmickOperation()
    {
        if (_mirrorBallrota == true) { return; }
        _mirrorBallrota = true;
        var opeSeq = DOTween.Sequence();
        opeSeq.Append(transform.DOMoveY(_fallTransY, _allGimmickTime / 4))
            .AppendInterval(_allGimmickTime / 2)
            .AppendCallback(() => { Explosion(); })
            .Append(transform.DOMoveY(_upTransY, _allGimmickTime / 4))
            .OnUpdate(() =>
            {
                if (_mirrorBallrota)
                {
                    _mirrorBallObj.Rotate(0, 10, 0);
                }
            })
            .OnComplete(() => { _mirrorBallrota = false; });
        opeSeq.Play().SetLink(gameObject);
    }

    public void Explosion()
    {
        Debug.Log("ÇÒÅH");
        foreach (Transform trans in _mirrorBallObj.transform)
        {
            var rb = trans.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.AddExplosionForce(_expPower, _mirrorBallObj.position, _expRadius, 1, ForceMode.Impulse);
            }
        }
        _mirrorBallObj.DetachChildren();
    }

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.transform.SetParent(_mirrorBallObj.transform);
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
