using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureBookTest : MonoBehaviour
{
    [SerializeField, Header("スクロール用の親Gameobject")] private GameObject _scrollGameObject;
    public GameObject ScrollGameObject => _scrollGameObject;

    [SerializeField, Header("1スクロールの時間")] private float _scrollTime;

    public float ScrollTime => _scrollTime;

    [SerializeField] private float _y = 0; //スクロール時の移動量

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
            //var character = Instantiate(_pictureBookDataTests[i].Character); 3dObjectでやる場合(コードの変更要)
            //character.transform.parent = this.transform;
            //_characterList.Add(character);
            //character.SetActive(false);

            _pictureBookDataTests[i].Texts[0].text = $"{_pictureBookDataTests[i].CharacterName}";
            _pictureBookDataTests[i].Texts[1].text = $"待ち時間 : {_pictureBookDataTests[i].CharacterTime}";
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
    [SerializeField, Header("GameObjectでやる場合のプレハブ")] private GameObject _character;

    public GameObject Character => _character;

    [SerializeField, Header("使用するText")] private Text[] _texts;　//Textを3つ入れる(name,time,ditail)

    public Text[] Texts => _texts;

    [SerializeField, Header("キャラクターの名前")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("キャラクターの待ち時間")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("キャラクターの説明")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
