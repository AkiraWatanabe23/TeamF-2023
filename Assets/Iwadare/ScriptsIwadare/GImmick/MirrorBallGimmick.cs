using DG.Tweening;
using UnityEngine;


///注意！ミラーボールにアイテムを巻き込ませるには
///アイテムに『IwadareGimmickScripts』をアタッチしないと巻き込めません。

public class MirrorBallGimmick : BaseGimmickArea
{
    [Header("ミラーボールのTransform")]
    [SerializeField] Transform _mirrorBallObj;
    [Header("落ちる場所")]
    [SerializeField] float _fallTransY = 0.8f;
    [Header("初期位置")]
    [SerializeField] float _upTransY = 3f;
    [Header("ギミック作動総時間")]
    [SerializeField] float _allGimmickTime = 4f;
    [Header("周りのアイテムがはじけ飛ぶ時の設定")]
    [SerializeField] float _expPower = 3f;
    [SerializeField] float _expRadius = 3f;
    Vector3 _gizmoSize = new Vector3(0.3f, 0.3f, 0.3f);
    bool _mirrorBallrota = false;

    private void Start()
    {
        var pos = transform.position;
        pos.y = _upTransY;
        transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, _upTransY, transform.position.z), _gizmoSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, _fallTransY, transform.position.z), _gizmoSize);
    }

    /// <summary>ミラーボールの一連の動き</summary>
    public override void GimmickOperation()
    {
        //ギミック作動中はreturnを返す。
        if (_mirrorBallrota == true) { return; }

        _mirrorBallrota = true;
        //ギミックの動きのSequence
        var opeSeq = DOTween.Sequence();
        //落ちる
        opeSeq.Append(transform.DOMoveY(_fallTransY, _allGimmickTime / 4))
            .AppendInterval(_allGimmickTime / 2)
            //周りのアイテムを吹っ飛ばす。
            .AppendCallback(() => { Explosion(); })
            //初期位置に戻る。
            .Append(transform.DOMoveY(_upTransY, _allGimmickTime / 4))
            .OnUpdate(() =>
            {
                //sequence作動中ミラーボールが回る。
                if (_mirrorBallrota)
                {
                    _mirrorBallObj.Rotate(0, 10, 0);
                }
            })
            //CompleteでGimmickの再作動が可能に。
            .OnComplete(() => { _mirrorBallrota = false; });
        opeSeq.Play().SetLink(gameObject);
    }

    /// <summary>周りのアイテムをふき飛ばす。</summary>
    public void Explosion()
    {
        foreach (Transform trans in _mirrorBallObj.transform)
        {
            var rb = trans.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.AddExplosionForce(_expPower, _mirrorBallObj.position, _expRadius, 1, ForceMode.Impulse);
            }
        }
        //自身の子オブジェクトを全て外す。
        _mirrorBallObj.DetachChildren();
    }


    private void OnTriggerEnter(Collider other)
    {
        //当たったオブジェクトにIwadareGimmickScriptsが入ってなかったらreturnを返す。
        if (!other.TryGetComponent<IwadareGimmickScripts>(out var tmp)) { return; }
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //自身の子オブジェクトにしてグルグルさせる。
            rb.transform.SetParent(_mirrorBallObj.transform);
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
