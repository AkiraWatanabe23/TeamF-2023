using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonScriopts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
{
    Button _button;
    RectTransform _rectTransform;
    [SerializeField] Color _enterColor = Color.white;
    [SerializeField] Color _pushUpColor = Color.white;
    [Header("DebugLog�̕\���؂�ւ�")]
    [SerializeField] bool _displayLogBool;
    [Header("�}�E�X�������Ă����Ƃ��̑傫��")]
    [SerializeField] float _enterScale = 1.1f;
    [Header("�v�b�V�����̃{�^���̑傫��")]
    [SerializeField] float _pushScale = 0.8f;
    [Header("���ꂼ��̑傫����ς���Ƃ��̎���")]
    [SerializeField] float _selectTime = 0.3f;
    Color _defaultColor = Color.white;
    Vector3 _tmpScale;
    void Start()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
        _tmpScale = _rectTransform.localScale;
        _defaultColor = _button.image.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DebugLogUtility.PrankLog("�|�|�|�|�|�`�I",_displayLogBool);

        _rectTransform.DOScale(_tmpScale * _pushScale, _selectTime).SetLink(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DebugLogUtility.PrankLog("���̃N���b�N�͂��I���E�C�`�B�B�B�B�B�I",_displayLogBool);
        _rectTransform.localScale = _tmpScale;
        _button.image.color = _pushUpColor;
        _rectTransform.DOScale(_tmpScale,_selectTime).SetLink(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.image.color = _enterColor;
        _rectTransform.DOScale(_tmpScale * _enterScale, _selectTime).SetLink(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button.image.color = _defaultColor;
        _rectTransform.DOScale(_tmpScale, _selectTime).SetLink(gameObject);
    }
}
