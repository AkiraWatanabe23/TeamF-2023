using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class IllustratedBook : MonoBehaviour
{
    [SerializeField, Header("�X�N���[���p�̐eGameobject")] private GameObject _scrollGameObject;
    public GameObject ScrollGameObject => _scrollGameObject;

    [SerializeField, Header("1�X�N���[���̎���")] private float _scrollTime;
    public float ScrollTime => _scrollTime;

    [SerializeField] private float _y = 0; //�X�N���[�����̈ړ���

    [SerializeField] private bool _inputFlag = false;

    [SerializeField, Header("enabled��؂�ւ��邽��")] private bool _activeBool = false;

    [SerializeField, Header("Canvas�����܂�")] private Canvas _canvas;

    private float _scroll = 0;

    private int _scrollCount = 0;

    [SerializeField, Header("Dotween�̃v���X�ړ���")] private float _plusMoveY = 1100f;

    [SerializeField, Header("Dotween�̃}�C�i�X�ړ���")] private float _minusMoveY = -1100f;

    [SerializeField] IllustratedBookData[] _illustratedBookDatas = default;

    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _illustratedBookDatas.Length; i++)
        {
            _illustratedBookDatas[i].CharacterTexts[0].text = $"{_illustratedBookDatas[i].CharacterName}";
            _illustratedBookDatas[i].CharacterTexts[1].text = $"�҂����� : {_illustratedBookDatas[i].CharacterTime}";
            _illustratedBookDatas[i].CharacterTexts[2].text = $"{_illustratedBookDatas[i].CharacterDetail}";
        }

        _y = GetComponent<RectTransform>().anchoredPosition.y;
        ActiveChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inputFlag)
        {
            _scroll = Input.GetAxis("Mouse ScrollWheel");

            if (_scroll > 0 && _scrollCount < _illustratedBookDatas.Length - 1)
            {
                StartTween(_plusMoveY);
                _scrollCount++;
            }
            else if (_scroll < 0 && _scrollCount > 0)
            {
                StartTween(_minusMoveY);
                _scrollCount--;
            }
        }
    }

    private void StartTween(float yOffset)
    {
        _inputFlag = true;

        var moveAnim = ScrollGameObject.transform.DOLocalMoveY(_y + yOffset, ScrollTime).SetLink(gameObject);
        moveAnim.OnComplete(() =>
        {
            _y += yOffset;
            _inputFlag = false;
        });
    }

    public void ActiveChange()
    {
        _activeBool = !_activeBool;
        if (_activeBool) { _canvas.enabled = true; }
        else { _canvas.enabled = false; }
    }
}
[System.Serializable]
public class IllustratedBookData
{
    [SerializeField, Header("�g�p����Text")] private Text[] _charactertexts;�@//Text��3�����(name,time,ditail)

    public Text[] CharacterTexts => _charactertexts;

    [SerializeField, Header("�L�����N�^�[�̖��O")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("�L�����N�^�[�̑҂�����")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("�L�����N�^�[�̐���")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
