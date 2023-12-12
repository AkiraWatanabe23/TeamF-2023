using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEditor;

public class MoneyTextScripts : MonoBehaviour
{
    [Header("TextMeshProが子にいるWorldCanvas")]
    [SerializeField] Canvas _moneyTextCanvasPrefab;
    [Header("Instantiateした際の物の高さの差")]
    [SerializeField] float _insUpDifference = 1f;
    [Header("テキストのy方向への移動の終点と始点の差")]
    [SerializeField] float _moveUpDifference = 0.5f;
    [SerializeField] float _fadeTime = 3f;
    [SerializeField] float _destroyTime = 5f;
    [SerializeField] bool _targetCamera = true;
    [Header("ここより下はデバック用(アタッチしなくて良い)")]
    [SerializeField] Button _button;
    [SerializeField] int _testnum;
    [SerializeField] Transform _custmer;

    private void Start()
    {
        if(_button != null)
        {
            _button.onClick.AddListener(() => MoneyText(_testnum,_custmer.position));
        }
    }

    public void MoneyText(int moneyCount,Vector3 custmerTransform)
    {
        custmerTransform.y = custmerTransform.y + _insUpDifference;
        if (_moneyTextCanvasPrefab != null)
        {
            var moneyText = Instantiate(_moneyTextCanvasPrefab, custmerTransform, Quaternion.identity);
            var moneyTextMeshPro = moneyText.GetComponentInChildren<TextMeshProUGUI>();
            var targetPos = Camera.main.transform.rotation;
            if (_targetCamera)
            {
                moneyText.transform.rotation = targetPos;
            }
            else
            {
                var rota = moneyText.transform.rotation;
                rota.y += 180f;
                moneyText.transform.rotation = rota;
            }
            ColorChangeText(moneyTextMeshPro,moneyCount);
            TextMove(moneyTextMeshPro,moneyCount,custmerTransform);
            Destroy(moneyText.gameObject, _destroyTime);
        }
    }

    void ColorChangeText(TextMeshProUGUI moneyText, int moneyCount)
    {
        moneyText.color = moneyCount >= 0 ? Color.yellow : Color.red;
    }

    void TextMove(TextMeshProUGUI moneyText,int moneyCount,Vector3 custmerTransform)
    {
        //+か-で表示するテキストの内容を変える三項演算子。
        moneyText.text = moneyCount >= 0 ? $"+${moneyCount}" : $"-${Math.Abs(moneyCount)}";
        var fadeSeq = DOTween.Sequence();
        fadeSeq.Append(DOTween.ToAlpha(
            () => moneyText.color,
            color => moneyText.color = color,
            0f, _fadeTime)
            ).Join(moneyText.transform.DOMoveY(custmerTransform.y + _moveUpDifference, _fadeTime));
        fadeSeq.Play().SetLink(moneyText.gameObject);
    }
}
