using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureBook : MonoBehaviour
{
    [SerializeField, Header("名前を記入するためのText")] private Text _characterNameText;
    public Text CharacterNameText => _characterNameText;

    [SerializeField, Header("待ち時間を記入するためのText")] private Text _characterTimeText;
    public Text CharacterTimeText => _characterTimeText;

    [SerializeField, Header("説明文を記入するためのText")] private Text _characterText;
    public Text CharacterText => _characterText;

    [SerializeField, Header("キャラクターの表示位置")] private Vector3 _characterPosition;
    public Vector3 CharacterPosition => _characterPosition;

    [SerializeField] private PictureBookData[] _bookDatas = default;

    private List<GameObject> _characterList = new List<GameObject>();

    private int _charactorCount = 0; // %で割る用の変数

    private bool _clickChange = false; //どちらのボタンが押されたかどうか判定する

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

    public void Click(bool click) //NextButton,BackButtonにアタッチ
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
        CharacterTimeText.text = $"待ち時間 : {_bookDatas[count].CharacterTime}秒";
        CharacterText.text = $"{_bookDatas[count].CharacterDetail}";
    }
}

[CreateAssetMenu(fileName = "PictureBookData",menuName = "PictureBook")]
[Serializable]
public class PictureBookData : ScriptableObject
{
    [SerializeField, Header("GameObjectでやる場合のプレハブ")] private GameObject _character;

    public GameObject Character => _character;

    [SerializeField, Header("イラストでやる場合(もしイラストが完成したら)のプレハブ")] private Image _characterImage;

    public Image CharacterImage => _characterImage;

    [SerializeField, Header("キャラクターの名前")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("キャラクターの待ち時間")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("キャラクターの説明")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
