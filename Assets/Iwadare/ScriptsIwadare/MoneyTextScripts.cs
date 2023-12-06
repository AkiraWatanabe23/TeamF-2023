using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyTextScripts : MonoBehaviour
{
    [SerializeField] TextMesh _moneyTextPrefab;
    [Header("Instantiateした際の物の高さの差")]
    [SerializeField] float _insUpDifference = 1f;
    [Header("テキストのy方向への移動の終点と始点の差")]
    [SerializeField] float _moveUpDifference = 0.5f;
    [SerializeField] float _fadeTime = 3f;
    [SerializeField] float _destroyTime = 5f;
    [SerializeField] bool _textReverse = true;
    [Header("ここより下はデバック用(アタッチしなくて良い)")]
    [SerializeField] Button _button;
    [SerializeField] Transform _custmer;

    private void Start()
    {
        if(_button != null)
        {
            _button.onClick.AddListener(() => MoneyText(500,_custmer.position));
        }
    }

    public void MoneyText(int moneyCount,Vector3 custnerTransform)
    {
        custnerTransform.y = custnerTransform.y + _insUpDifference;
        var moneyText = Instantiate(_moneyTextPrefab, custnerTransform, Quaternion.identity);
        moneyText.transform.LookAt(Camera.main.transform);
        if (_textReverse) { moneyText.transform.rotation = new Quaternion(0f, 180f, 0f, 0f); }
        //+か-で表示するテキストの内容を変える三項演算子。
        moneyText.text = moneyCount > 0 ? $"+${moneyCount}" : $"-${Math.Abs(moneyCount)}";
        var fadeSeq = DOTween.Sequence();
        fadeSeq.Append(DOTween.ToAlpha(
            () => moneyText.color,
            color => moneyText.color = color,
            0f, _fadeTime)
            ).Join(moneyText.transform.DOMoveY(custnerTransform.y + _moveUpDifference ,_fadeTime));
        fadeSeq.Play().SetLink(moneyText.gameObject);
        Destroy(moneyText.gameObject,_destroyTime);
    }
}
