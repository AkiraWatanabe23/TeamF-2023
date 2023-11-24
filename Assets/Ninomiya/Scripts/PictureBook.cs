using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureBook : MonoBehaviour
{
    [SerializeField, Header("���O���L�����邽�߂�Text")] private Text _characterNameText;
    public Text CharacterNameText => _characterNameText;

    [SerializeField, Header("�҂����Ԃ��L�����邽�߂�Text")] private Text _characterTimeText;
    public Text CharacterTimeText => _characterTimeText;

    [SerializeField, Header("���������L�����邽�߂�Text")] private Text _characterText;
    public Text CharacterText => _characterText;

    [SerializeField, Header("�L�����N�^�[�̕\���ʒu")] private Vector3 _characterPosition;
    public Vector3 CharacterPosition => _characterPosition;

    [SerializeField] private PictureBookData[] _bookDatas = default;

    private List<GameObject> _characterList = new List<GameObject>();

    private int _charactorCount = 0; // %�Ŋ���p�̕ϐ�

    private bool _clickChange = false; //�ǂ���̃{�^���������ꂽ���ǂ������肷��

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _bookDatas.Length; i++)
        {
            var character = Instantiate(_bookDatas[i].Character, CharacterPosition, Quaternion.identity);
            character.transform.parent = this.transform;
            _characterList.Add(character);
            character.SetActive(false);
        }
        CharacterChange(_charactorCount);
    }

    public void Click(bool click) //NextButton,BackButton�ɃA�^�b�`
    {
        _characterList[_charactorCount].SetActive(false);

        if (click == _clickChange) { _charactorCount++; }
        else
        {
            if(_clickChange != false)
            {
                _clickChange = !_clickChange;
            }
            _charactorCount--;
        }

        if(_charactorCount < 0) { _charactorCount = _characterList.Count - 1; }
        else { _charactorCount %= _characterList.Count; }
        CharacterChange(_charactorCount);
    }

    public void CharacterChange(int count)
    {
        _characterList[count].SetActive(true);
        CharacterNameText.text = $"{_bookDatas[count].CharacterName}";
        CharacterTimeText.text = $"�҂����� : {_bookDatas[count].CharacterTime}�b";
        CharacterText.text = $"{_bookDatas[count].CharacterDetail}";
    }
}

[CreateAssetMenu(fileName = "PictureBookData",menuName = "PictureBook")]
[Serializable]
public class PictureBookData : ScriptableObject
{
    [SerializeField, Header("GameObject�ł��ꍇ�̃v���n�u")] private GameObject _character;

    public GameObject Character => _character;

    [SerializeField, Header("�C���X�g�ł��ꍇ(�����C���X�g������������)�̃v���n�u")] private Image _characterImage;

    public Image CharacterImage => _characterImage;

    [SerializeField, Header("�L�����N�^�[�̖��O")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("�L�����N�^�[�̑҂�����")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("�L�����N�^�[�̐���")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
