using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBook : MonoBehaviour
{
    [SerializeField, Header("スクロール用の親Gameobject")] private GameObject _scrollGameObject;
    public GameObject ScrollGameObject => _scrollGameObject;

    [SerializeField, Header("1スクロールの時間")] private float _scrollTime;
    public float ScrollTime => _scrollTime;

    [SerializeField] private float _y = 0; //スクロール時の移動量

    [SerializeField] private bool _inputFlag = false;

    [SerializeField, Header("enabledを切り替えるため")] private bool _activeBool = false;

    [SerializeField, Header("Canvasを入れます")] private Canvas _canvas;

    private float _scroll = 0;

    private int _scrollCount = 0;

    [SerializeField, Header("Dotweenのプラス移動量")] private float _plusMoveY = 1100f;

    [SerializeField, Header("Dotweenのマイナス移動量")] private float _minusMoveY = -1100f;

    [SerializeField] IllustratedBookData[] _illustratedBookDatas = default;

    [SerializeField, Header("slider")] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _illustratedBookDatas.Length; i++)
        {
            _illustratedBookDatas[i].CharacterTexts[0].text = $"{_illustratedBookDatas[i].CharacterName}";
            _illustratedBookDatas[i].CharacterTexts[1].text = $"{_illustratedBookDatas[i].CharacterTime}";
            _illustratedBookDatas[i].CharacterTexts[2].text = $"{_illustratedBookDatas[i].CharacterDetail}";
        }

        _y = GetComponent<RectTransform>().anchoredPosition.y;
        ActiveChange();
        _slider.maxValue = _illustratedBookDatas.Length - 1;
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
                _slider.value++;
            }
            else if (_scroll < 0 && _scrollCount > 0)
            {
                StartTween(_minusMoveY);
                _scrollCount--;
                _slider.value--;
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
    [SerializeField, Header("使用するText")] private Text[] _charactertexts;　//Textを3つ入れる(name,time,ditail)

    public Text[] CharacterTexts => _charactertexts;

    [SerializeField, Header("キャラクターの名前")] private string _characterName;

    public string CharacterName => _characterName;
    [TextArea(1, 2)]
    [SerializeField, Header("キャラクターの待ち時間")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 6)]
    [SerializeField, Header("キャラクターの説明")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
