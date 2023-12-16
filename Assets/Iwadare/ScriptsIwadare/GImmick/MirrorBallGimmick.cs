using DG.Tweening;
using UnityEngine;


///���ӁI�~���[�{�[���ɃA�C�e�����������܂���ɂ�
///�A�C�e���ɁwIwadareGimmickScripts�x���A�^�b�`���Ȃ��Ɗ������߂܂���B

public class MirrorBallGimmick : BaseGimmickArea
{
    [Header("�~���[�{�[����Transform")]
    [SerializeField] Transform _mirrorBallObj;
    [Header("������ꏊ")]
    [SerializeField] float _fallTransY = 0.8f;
    [Header("�����ʒu")]
    [SerializeField] float _upTransY = 3f;
    [Header("�M�~�b�N�쓮������")]
    [SerializeField] float _allGimmickTime = 4f;
    [Header("����̃A�C�e�����͂�����Ԏ��̐ݒ�")]
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

    /// <summary>�~���[�{�[���̈�A�̓���</summary>
    public override void GimmickOperation()
    {
        //�M�~�b�N�쓮����return��Ԃ��B
        if (_mirrorBallrota == true) { return; }

        _mirrorBallrota = true;
        //�M�~�b�N�̓�����Sequence
        var opeSeq = DOTween.Sequence();
        //������
        opeSeq.Append(transform.DOMoveY(_fallTransY, _allGimmickTime / 4))
            .AppendInterval(_allGimmickTime / 2)
            //����̃A�C�e���𐁂���΂��B
            .AppendCallback(() => { Explosion(); })
            //�����ʒu�ɖ߂�B
            .Append(transform.DOMoveY(_upTransY, _allGimmickTime / 4))
            .OnUpdate(() =>
            {
                //sequence�쓮���~���[�{�[�������B
                if (_mirrorBallrota)
                {
                    _mirrorBallObj.Rotate(0, 10, 0);
                }
            })
            //Complete��Gimmick�̍č쓮���\�ɁB
            .OnComplete(() => { _mirrorBallrota = false; });
        opeSeq.Play().SetLink(gameObject);
    }

    /// <summary>����̃A�C�e�����ӂ���΂��B</summary>
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
        //���g�̎q�I�u�W�F�N�g��S�ĊO���B
        _mirrorBallObj.DetachChildren();
    }


    private void OnTriggerEnter(Collider other)
    {
        //���������I�u�W�F�N�g��IwadareGimmickScripts�������ĂȂ�������return��Ԃ��B
        if (!other.TryGetComponent<IwadareGimmickScripts>(out var tmp)) { return; }
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //���g�̎q�I�u�W�F�N�g�ɂ��ăO���O��������B
            rb.transform.SetParent(_mirrorBallObj.transform);
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
