using Alpha;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UFOGimmick : MonoBehaviour
{
    [Header("初期移動時の値")]
    [Tooltip("最初にステージ上まで移動する時間")]
    [SerializeField]
    private float _moveDuration = 1f;
    [Tooltip("移動先のオフセット（生成地点からの移動）")]
    [SerializeField]
    private Vector3 _moveOffset = default;

    [Tooltip("移動後に上下する幅")]
    [Min(0.1f)]
    [SerializeField]
    private float _upValue = 1f;

    [Header("吸い上げ時の動きに対する値")]
    [Tooltip("UFOが一度に最大いくつのオブジェクトを検出するか")]
    [SerializeField]
    private int _maxCastCount = 10;
    [Tooltip("吸い上げ地点のオフセット（UFOより下の位置）")]
    [SerializeField]
    private Vector3 _suckUpOffset = Vector3.zero;
    [Tooltip("オブジェクトが吸い上げられるスピード")]
    [SerializeField]
    private float _suckUpDuration = 1f;

    [Header("墜落時の動きに対する値")]
    [Tooltip("揺れる、落ちる速度")]
    [SerializeField]
    private float _tweenSpeed = 1f;
    [Tooltip("攻撃を受けてから消えるまでの時間")]
    [SerializeField]
    private float _activeFalseInterval = 2f;
    [Tooltip("墜落時の振れ幅（横）")]
    [SerializeField]
    private float _swayValue = 10f;
    [Tooltip("墜落時の揺れ幅（回転）")]
    [SerializeField]
    private float _rotateValue = 45f;

    private Transform _transform = default;

    /// <summary> 初期位置 </summary>
    private Vector3 _initPosition = Vector3.zero;
    /// <summary> BoxCast用のパラメータ </summary>
    private Vector3 _halfExtents = Vector3.zero;

    /// <summary> 移動済かどうか </summary>
    private bool _isMoved = false;

    /// <summary> 吸い上げ判定にかかったオブジェクトを格納する </summary>
    private RaycastHit[] _suckUpDatas = default;

    private Sequence _sequence = default;

    private IEnumerator Start()
    {
        yield return Initialize();

        Movement();
    }

    /// <summary> 初期データのセットアップ </summary>
    private IEnumerator Initialize()
    {
        _transform = transform;
        _initPosition = _transform.position;

        if (TryGetComponent(out BoxCollider collider)) { _halfExtents = collider.size / 2f; }
        else { _halfExtents = _transform.localScale; }

        _suckUpDatas = new RaycastHit[_maxCastCount];

        _isMoved = false;
#if UNITY_EDITOR
        Debug.Log("Finish Initialized");
#endif

        yield return null;
    }

    private void Movement()
    {
        _sequence ??= DOTween.Sequence();

        _sequence.
            AppendCallback(() =>
            {
                Cri.PlaySE3D(_transform.position, "SE_UFO_1_Long", "CueSheet_SE4");
            }).
            Append(_transform.DOMove(_initPosition + _moveOffset, _moveDuration)).
            AppendCallback(() =>
            {
                _isMoved = true;
                _transform.DOMoveY(_upValue, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }).
            SetLink(gameObject);
    }

    private void Update()
    {
        if (!_isMoved) { return; }

        //回収対象探す
        ItemSearch();
        //被攻撃判定（移動前、移動中はやらない）
        AttackedSearch();
    }

    /// <summary> 吸い上げる対象があるか探す </summary>
    private void ItemSearch()
    {
        var count = Physics.BoxCastNonAlloc(_transform.position, _halfExtents, Vector3.down, _suckUpDatas, Quaternion.identity);
        if (count > 0)
        {
            for (int i = 0; i < count; i++) { SuckUp(_suckUpDatas[i].collider.gameObject); }
        }
    }

    /// <summary> 吸い上げる </summary>
    private void SuckUp(GameObject target)
    {
        if (target.TryGetComponent(out ThrowedItem item))
        {
#if UNITY_EDITOR
            Debug.Log("見つけた");
#endif
            //ここに吸い上げ処理
            target.transform.
                DOMove(_transform.position + _suckUpOffset, _suckUpDuration).
                OnComplete(() =>
                {
#if UNITY_EDITOR
                    Debug.Log("tween finish");
#endif
                    target.SetActive(false);
                }).
                SetLink(target);
        }
    }

    /// <summary> Playerからの追撃がないか調べる </summary>
    private void AttackedSearch()
    {
        if (Physics.BoxCast(_transform.position, _halfExtents, Vector3.zero, out RaycastHit hit, Quaternion.identity))
        {
            if (hit.collider.gameObject.TryGetComponent(out ThrowedItem _)) { Crash(); }
        }
    }

    private void Crash()
    {
#if UNITY_EDITOR
        Debug.Log("攻撃を受けた！！墜落");
#endif
        _sequence = DOTween.Sequence();
        _sequence.
            AppendCallback(() =>
            {
                _transform.DOMoveX(_swayValue, _tweenSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                _transform.DOMoveY(-1f, _tweenSpeed).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

                _transform.DORotate(new Vector3(0f, 0f, _rotateValue), _tweenSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }).
            AppendInterval(_activeFalseInterval).
            AppendCallback(() =>
            {
                ChangeActiveSelf(false);
            });
    }

    private void ChangeActiveSelf(bool flag) { gameObject.SetActive(flag); }
}
