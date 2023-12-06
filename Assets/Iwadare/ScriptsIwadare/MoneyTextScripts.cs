using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyTextScripts : MonoBehaviour
{
    [SerializeField] TextMesh _moneyTextPrefab;
    [Header("Instantiate�����ۂ̕��̍����̍�")]
    [SerializeField] float _insUpDifference = 1f;
    [Header("�e�L�X�g��y�����ւ̈ړ��̏I�_�Ǝn�_�̍�")]
    [SerializeField] float _moveUpDifference = 0.5f;
    [SerializeField] float _fadeTime = 3f;
    [SerializeField] float _destroyTime = 5f;
    [SerializeField] bool _textReverse = true;
    [Header("������艺�̓f�o�b�N�p(�A�^�b�`���Ȃ��ėǂ�)")]
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
        //+��-�ŕ\������e�L�X�g�̓��e��ς���O�����Z�q�B
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
