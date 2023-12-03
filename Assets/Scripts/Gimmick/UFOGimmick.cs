using Alpha;
using DG.Tweening;
using UnityEngine;

public class UFOGimmick : MonoBehaviour
{
    [Tooltip("UFOが一度に最大いくつのオブジェクトを検出するか")]
    [SerializeField]
    private int _maxCastCount = 10;
    [SerializeField]
    private Animator _animator = default;
    [Tooltip("吸い上げ地点のoffset")]
    [SerializeField]
    private Vector3 _offset = Vector3.zero;
    [Tooltip("移動先のゴール")]
    [SerializeField]
    private Transform _moveTarget = default;

    [SerializeField]
    private bool _debug = false;

    private Transform _transform = default;
    private Vector3 _halfExtents = Vector3.zero;

    private RaycastHit[] _suckUpDatas = default;

    private void Start()
    {
        Initialize();

        if (_debug) { Movement(); }
    }

    /// <summary> 初期データのセットアップ </summary>
    private void Initialize()
    {
        _transform = transform;

        if (_animator == null)
        {
            if (!gameObject.TryGetComponent(out _animator)) { _animator = gameObject.AddComponent<Animator>(); }
        }

        if (gameObject.TryGetComponent(out BoxCollider collider)) { _halfExtents = collider.size / 2f; }
        else { _halfExtents = _transform.localScale; }

        _suckUpDatas = new RaycastHit[_maxCastCount];
    }

    private void Movement()
    {
        transform.
            DOMove(_moveTarget.position, 2f).
            OnComplete(() =>ItemSearch());
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
                DOMove(_transform.position + _offset, 1f).
                OnComplete(() =>
                {
                    Debug.Log("tween finish");
                    target.SetActive(false);
                });
        }
    }

    private void PlayAnimation(string animationName)
    {
        var clips = _animator.runtimeAnimatorController.animationClips;
        //渡された名前のStateがAnimatorに含まれているか調べる
        bool containsTargetState = false;
        foreach (var clip in clips)
        {
            if (clip.name == animationName) { containsTargetState = true; break; }
        }
        if (!containsTargetState) { Debug.Log($"Animation Play failed : {animationName} State is not found."); return; }

        _animator.Play(animationName);
    }

    /// <summary> Playerからの追撃がないか調べる </summary>
    private void AttackedSearch()
    {
        if (Physics.BoxCast(_transform.position, _halfExtents, Vector3.zero, out RaycastHit hit, Quaternion.identity))
        {
            if (hit.collider.gameObject.TryGetComponent(out ThrowedItem _))
            {
#if UNITY_EDITOR
                Debug.Log("攻撃を受けた！！墜落");
#endif
                PlayAnimation("Crash");
            }
        }
    }

    /// <summary> AnimationEventとかで呼び出す用 </summary>
    public void ChangeActive(bool flag) { gameObject.SetActive(flag); }
}
