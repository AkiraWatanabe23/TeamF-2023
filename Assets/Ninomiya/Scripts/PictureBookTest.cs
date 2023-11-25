using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureBookTest : MonoBehaviour
{
    [SerializeField, Header("�X�N���[���p�̐eGameobject")] private GameObject _scrollGameObject;
    public GameObject ScrollGameObject => _scrollGameObject;

    [SerializeField, Header("1�X�N���[���̎���")] private float _scrollTime;

    public float ScrollTime => _scrollTime;

    [SerializeField] private float _y = 0; //�X�N���[�����̈ړ���

    [SerializeField] private bool _inputFlag = false;

    [SerializeField] PictureBookDataTest[] _pictureBookDataTests = default;

    private List<GameObject> _characterList = new List<GameObject>();

    private float _scroll = 0;

    private int _scrollCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _pictureBookDataTests.Length; i++)
        {
            //var character = Instantiate(_pictureBookDataTests[i].Character); 3dObject�ł��ꍇ(�R�[�h�̕ύX�v)
            //character.transform.parent = this.transform;
            //_characterList.Add(character);
            //character.SetActive(false);

            _pictureBookDataTests[i].Texts[0].text = $"{_pictureBookDataTests[i].CharacterName}";
            _pictureBookDataTests[i].Texts[1].text = $"�҂����� : {_pictureBookDataTests[i].CharacterTime}";
            _pictureBookDataTests[i].Texts[2].text = $"{_pictureBookDataTests[i].CharacterDetail}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inputFlag)
        {
            _scroll = Input.GetAxis("Mouse ScrollWheel");

            if (_scroll > 0 && _scrollCount < _pictureBookDataTests.Length - 2)
            {
                StartTween(555f);
                _scrollCount++;
            }
            else if (_scroll < 0 && _scrollCount > 0)
            {
                StartTween(-555f);
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
}

[CreateAssetMenu(fileName = "PictureBookDataTest", menuName = "PictureBookTest")]
[Serializable]
public class PictureBookDataTest : ScriptableObject
{
    [SerializeField, Header("GameObject�ł��ꍇ�̃v���n�u")] private GameObject _character;

    public GameObject Character => _character;

    [SerializeField, Header("�g�p����Text")] private Text[] _texts;�@//Text��3�����(name,time,ditail)

    public Text[] Texts => _texts;

    [SerializeField, Header("�L�����N�^�[�̖��O")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("�L�����N�^�[�̑҂�����")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("�L�����N�^�[�̐���")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
