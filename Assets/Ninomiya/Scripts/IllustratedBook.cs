using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class IllustratedBook : MonoBehaviour
{
    [SerializeField, Header("スクロール用の親Gameobject")] private GameObject _scrollGameObject;
    public GameObject ScrollGameObject => _scrollGameObject;

    [SerializeField, Header("1スクロールの時間")] private float _scrollTime;
    public float ScrollTime => _scrollTime;

    [SerializeField] private float _y = 0; //スクロール時の移動量

    [SerializeField] private bool _inputFlag = false;

    private float _scroll = 0;

    private int _scrollCount = 0;

    [SerializeField, Header("Dotweenのプラス移動量")] private float _plusMoveY = 1100f;

    [SerializeField, Header("Dotweenのマイナス移動量")] private float _minusMoveY = -1100f;

    [SerializeField] IllustratedBookData[] _illustratedBookDatas = default;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _illustratedBookDatas.Length; i++)
        {
            _illustratedBookDatas[i].CharacterTexts[0].text = $"{_illustratedBookDatas[i].CharacterName}";
            _illustratedBookDatas[i].CharacterTexts[1].text = $"待ち時間 : {_illustratedBookDatas[i].CharacterTime}";
            _illustratedBookDatas[i].CharacterTexts[2].text = $"{_illustratedBookDatas[i].CharacterDetail}";
        }

        _y = GetComponent<RectTransform>().anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inputFlag)
        {
            _scroll = Input.GetAxis("Mouse ScrollWheel");

            if (_scroll > 0 && _scrollCount < _illustratedBookDatas.Length - 1)
            {
                StartTween(1100f);
                _scrollCount++;
            }
            else if (_scroll < 0 && _scrollCount > 0)
            {
                StartTween(-1100f);
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

[CreateAssetMenu(fileName = "IllustratedBookData", menuName = "IllustratedBook")]
[System.Serializable]
public class IllustratedBookData : ScriptableObject
{
    [SerializeField, Header("使用するText")] private Text[] _charactertexts;　//Textを3つ入れる(name,time,ditail)

    public Text[] CharacterTexts => _charactertexts;

    [SerializeField, Header("キャラクターの名前")] private string _characterName;

    public string CharacterName => _characterName;

    [SerializeField, Header("キャラクターの待ち時間")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 5)]
    [SerializeField, Header("キャラクターの説明")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
