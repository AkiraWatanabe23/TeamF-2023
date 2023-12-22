using Alpha;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;

public class UFOGimmick : MonoBehaviour
{
    [SerializeField]
    private Collider _suckCollider;
    [SerializeField]
    private Collider _hitCollider;
    [SerializeField]
    private float _suckRadius = 1.0f;
    [Header("撃破された際に吹っ飛んでく奴")]
    [SerializeField] GameObject _defeatedPrefab;
    [Header("ミニキャラキャラ関連")]
    [Min(1)]
    [SerializeField]
    private int _spawnCount = 1;
    [Tooltip("逃げるキャラクター")]
    [SerializeField]
    private GameObject _miniCharaPrefab = default;
    [Tooltip("キャラクターの生成範囲の半径")]
    [SerializeField]
    private float _runAwayRadius = 1f;
    [Tooltip("UFOのどれくらい下に生成するか")]
    [SerializeField]
    private float _runAwayOffset = -1f;
    [Tooltip("ミニキャラが逃げてから何秒後にUFOが動き出すか")]
    [SerializeField]
    private float _gimmickStartInterval = 1f;

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

    private Sequence _sequence = default;

    private readonly List<GameObject> _actors = new();
    // 吸われた際にトイーンを中断するため
    private readonly List<Sequence> _miniActorMoveSequences = new();

    /// <summary> UFOのSEの再生時インデックス </summary>
    private int _ufoSEIndex = 0;

    private IEnumerator Start()
    {
        yield return Initialize();

        MiniCharaMovement();

        yield return new WaitForSeconds(_gimmickStartInterval);

        Movement();
    }

    /// <summary> 初期データのセットアップ </summary>
    private IEnumerator Initialize()
    {
        _transform = transform;
        _initPosition = _transform.position;

        if (TryGetComponent(out BoxCollider collider)) { _halfExtents = Vector3.one * _suckRadius; }
        else { _halfExtents = _transform.localScale; }

        _suckCollider.OnTriggerEnterAsObservable()
            .Where(_ => _isMoved)
            .Subscribe(c => SuckUp(c.gameObject));

        _hitCollider.OnCollisionEnterAsObservable()
            .Where(_ => _isMoved)
            .Where(c => !c.gameObject.TryGetComponent(out SuckUpComponent _))
            .Subscribe(_ => Crash());

        _isMoved = false;
#if UNITY_EDITOR
        Debug.Log("Finish Initialized");
#endif

        yield return null;
    }

    /// <summary> 生成するミニキャラを動かす </summary>
    private void MiniCharaMovement()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            var circlePos = _runAwayRadius * Random.insideUnitCircle;
            var spawnPos = _transform.position + new Vector3(circlePos.x, _runAwayOffset, circlePos.y);

            StartCoroutine(SpawnAsync(spawnPos));
        }
    }

    IEnumerator SpawnAsync(Vector3 spawnPos)
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        var chara = Instantiate(_miniCharaPrefab, spawnPos, Quaternion.identity);
        _actors.Add(chara);

        var transform = chara.transform;
        var sequence = DOTween.Sequence();

        Vector3 actorOffset = _moveOffset;
        actorOffset.y = 0;

        sequence.
            Append(transform.DOMove(spawnPos + actorOffset, _moveDuration)).
            AppendInterval(1f).
            // 2番目の子にパーティクルのオブジェクトがあることが前提。止まったら煙のパーティクルを止める
            OnComplete(() => transform.GetChild(1).gameObject.SetActive(false)).
            SetLink(chara);

        _miniActorMoveSequences.Add(sequence);
    }

    private void Movement()
    {
        _sequence ??= DOTween.Sequence();

        _sequence.
            AppendCallback(() =>
            {
                _ufoSEIndex = CriAudioManager.Instance.SE.Play3D(_transform.position, "CusSheet_SE 7", "SE_UFO");
                //Cri.PlaySE3D(_transform.position, "SE_UFO");
            }).
            Append(_transform.DOMove(_initPosition + _moveOffset, _moveDuration)).
            AppendCallback(() =>
            {
                _isMoved = true;
                _transform.DOMoveY(_upValue, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetRelative();

                _actors.ForEach(a => SuckUp(a));
                _actors.Clear();
            }).
            SetLink(gameObject);
    }

    private void Update()
    {
        if (!_isMoved) { return; }

        //回収対象探す
        //ItemSearch();
        //被攻撃判定（移動前、移動中はやらない）
        //AttackedSearch();
    }

    /// <summary> 吸い上げる </summary>
    private void SuckUp(GameObject target)
    {
        if (target.TryGetComponent(out ThrowedItem _) || target.TryGetComponent(out SuckUpComponent _))
        {
#if UNITY_EDITOR
            Debug.Log("見つけた");
#endif
            //ここに吸い上げ処理
            target.transform.
                DOMove(_transform.position, _suckUpDuration).
                OnComplete(() =>
                {
#if UNITY_EDITOR
                    Debug.Log("tween finish");
#endif
                    target.SetActive(false);

                    // 1体目が吸われた時点でキャラ全員のトイーンを止めても大丈夫な気がする。
                    _miniActorMoveSequences.ForEach(seq => seq.Kill());
                    _miniActorMoveSequences.Clear();
                }).
                SetLink(target);
        }
    }

    private void Crash()
    {
#if UNITY_EDITOR
        Debug.Log("攻撃を受けた！！墜落");
#endif

        ChangeActiveSelf(false);
        GameObject instance = Instantiate(_defeatedPrefab, transform.position, Quaternion.identity);
        Vector3 dir = Vector3.forward * 6.0f + Vector3.up * 0.5f;
        instance.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);

        CriAudioManager.Instance.SE.Stop(_ufoSEIndex);
    }

    private void ChangeActiveSelf(bool flag) => gameObject.SetActive(flag);

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _halfExtents);
    }
}