using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinArea : BaseGimmickArea
{
    [Header("�����\���̎���")]
    [SerializeField] float _blinkingAllTime = 3f;
    [SerializeField] float _initBlinkingTime = 0.5f;
    [SerializeField] float _minBlinkingTime = 0.05f;
    [Header("�����̎���(�V�F�[�_�[���ꂽ�炢��Ȃ�����)")]
    [SerializeField] float _explosionTime = 1f;
    [Header("�����̗�")]
    [SerializeField] float _explosionPower = 30f;
    [Header("�����̏�֔�ԗ�")]
    [SerializeField] float _explosionUpPower = 30f;
    [Header("�����̐F(�V�F�[�_�[���ꂽ�炢��Ȃ�)")]
    [SerializeField] Renderer _explosionRenderer;
    Color _explosionColor = Color.red;
    [SerializeField]Color _normalExplosionColor = Color.clear;
    bool _explosionEnabled = true;
    [SerializeField] float _explosionRadius = 3f;
    public float ExplosionRadius => _explosionRadius;
    private void Awake()
    {
        _explosionRenderer.material.color = _normalExplosionColor;
    }
    private void Update()
    {
        if(GimmickOperationBool && _explosionEnabled)
        {
            StartCoroutine(Explosion());
            _explosionEnabled = false;

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    IEnumerator Explosion()
    {
        var blinking = 0f;
        var initBlinking = _initBlinkingTime;
        for (var time = 0f; time < _blinkingAllTime; time += Time.deltaTime)
        {
            blinking += Time.deltaTime;
            if (blinking > initBlinking)
            {
                if (_gimmickOpeRenderer.material.color == StartGimmickOpeColor)
                {
                    _gimmickOpeRenderer.material.color = StopGimmickOpeColor;
                }
                else
                {
                    _gimmickOpeRenderer.material.color = StartGimmickOpeColor;
                }
                blinking = 0f;
                initBlinking = Mathf.Max(_minBlinkingTime,initBlinking - 0.15f);
            }
            yield return null;
        }

        _explosionRenderer.material.color = _explosionColor;
        Collider[] cols = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (var col in cols)
        {
            if (!col.TryGetComponent<IwadareGimmickScripts>(out var tmp)) { continue; }
            var rb = col.GetComponent<Rigidbody>();
            if (rb != null) { rb.AddExplosionForce(_explosionPower, transform.position, ExplosionRadius,_explosionUpPower,ForceMode.Impulse); }
        }
        yield return new WaitForSeconds(_explosionTime);
        _explosionRenderer.material.color = _normalExplosionColor;
        ChangeAreaOperation();
        _explosionEnabled = true;
    }
}
