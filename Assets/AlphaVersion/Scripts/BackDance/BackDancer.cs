using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BackDancer : MonoBehaviour
{
    readonly int _facialNumberPropertyId = Shader.PropertyToID("_FacialNumber");

    [SerializeField] Transform _root;
    [SerializeField] Transform[] _models;
    [SerializeField] Renderer[] _renderers;
    [SerializeField] Transform _start;
    [SerializeField] Transform _goal;
    [SerializeField] float _width = 1.0f;
    [SerializeField] float _duration = 3.0f;
    [SerializeField] float _rotSpeed = 10.0f;

    void Start()
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.materials[1].SetFloat(_facialNumberPropertyId, 1.0f);
        }
    }

    public void Play()
    {
        DanceAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// 指定した位置まで移動
    /// </summary>
    async UniTask DanceAsync(CancellationToken token)
    {
        // お立ち台まで動かす
        _root.position = _start.position;
        float progress = 0;
        while (progress < 1)
        {
            _root.position = Vector3.Lerp(_start.position, _goal.position, progress);
            progress += Time.deltaTime;
            await UniTask.Yield();
        }
        _root.position = _goal.position;

        await UniTask.Yield();

        // セットで動かす
        //Sequence seq = DOTween.Sequence();
        //seq.Append(_root.DOLocalMoveZ(_width, _duration).SetEase(Ease.Linear))
        //    .Append(_root.DOLocalMoveZ(-_width, _duration * 2).SetEase(Ease.Linear))
        //    .Append(_root.DOLocalMoveZ(0, _duration).SetEase(Ease.Linear))
        //    .SetLoops(-1, LoopType.Restart)
        //    .SetLink(gameObject);

        // キャラ毎に動かす
        foreach (Transform t in _models)
        {
            ModelRotateAsync(t, token).Forget();
        }
    }

    /// <summary>
    /// Dotweenよりこっちのほうが楽
    /// </summary>
    async UniTaskVoid ModelRotateAsync(Transform t, CancellationToken token)
    {
        while (true)
        {
            t.Rotate(new Vector3(0, Time.deltaTime * _rotSpeed, 0));
            await UniTask.Yield(token);
        }
    }
}
