using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinArea : BaseGimmickArea
{
    [SerializeField] float _blinkingTime = 2f;
    [SerializeField] float _explosionTime = 1f;
    [SerializeField] float _explosionPower = 1000f;
    [SerializeField] float _explosionUpPower = 1000f;
    Color _explosionColor = Color.red;
    bool _explosionEnabled = true;
    private void Update()
    {
        if(OpeStopbool && _explosionEnabled)
        {
            StartCoroutine(Explosion());
            _explosionEnabled = false;

        }
    }

    IEnumerator Explosion()
    {
        var blinking = 0f;
        for(var time = 0f; time < _blinkingTime;time += Time.deltaTime)
        {
            blinking += Time.deltaTime;
            if (blinking > 0.2f)
            {
                if (_opeRenderer.material.color == StartOpeColor)
                {
                    _opeRenderer.material.color = StopOpeColor;
                }
                else
                {
                    _opeRenderer.material.color = StartOpeColor;
                }
                blinking = 0f;
            }
            yield return null;
        }

        _opeRenderer.material.color = _explosionColor;
        Collider[] cols = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach(var col in cols)
        {
            var rb = col.GetComponent<Rigidbody>();
            if (rb != null) { rb.AddExplosionForce(_explosionPower, transform.position, ExplosionRadius, _explosionUpPower); }
        }
        yield return new WaitForSeconds(_explosionTime);
        ChangeAreaOperation();
        _explosionEnabled = true;
    }
}
