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

    [SerializeField, Header("キャラクターの名前等入れるためのText")] private Text[] _characterTexts;

    private float _tmpCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        TextsChange(_scrollCount);
        _y = GetComponent<RectTransform>().anchoredPosition.y;
        ActiveChange();
        _slider.maxValue = _illustratedBookDatas.Length - 1;
        _slider.value = 0;
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
                _tmpCount++;
                _tmpCount = _scrollCount;
            }
            else if (_scroll < 0 && _scrollCount > 0)
            {
                StartTween(_minusMoveY);
                _scrollCount--;
                _slider.value--;
                _tmpCount--;
                _tmpCount = _scrollCount;
            }
        }
    }

    private void StartTween(float yOffset)
    {
        _inputFlag = true;
        _slider.interactable = false;
        var moveAnim = ScrollGameObject.transform.DOLocalMoveY(_y + yOffset, ScrollTime).SetLink(gameObject);
        moveAnim.OnComplete(() =>
        {
            _y += yOffset;
            _inputFlag = false;
            TextsChange(_scrollCount);
            _slider.interactable = true;
        });
    }

    public void ActiveChange()
    {
        _activeBool = !_activeBool;
        if (_activeBool) { _canvas.enabled = true; }
        else { _canvas.enabled = false; }
    }

    public void TextsChange(int count)
    {
        _characterTexts[0].text = $"{_illustratedBookDatas[count].CharacterName}";
        _characterTexts[1].text = $"{_illustratedBookDatas[count].CharacterTime}";
        _characterTexts[2].text = $"{_illustratedBookDatas[count].CharacterDetail}";
    }

    public void Slider()
    {
        if(!_inputFlag)
        {
            if(_tmpCount < _slider.value && _scrollCount < _illustratedBookDatas.Length - 1)
            {
                StartTween(_plusMoveY);
                _scrollCount++;
                _tmpCount++;
                _slider.value = _tmpCount;
            }
            else if(_scrollCount > 0)
            {
                StartTween(_minusMoveY);
                _scrollCount--;
                _tmpCount--;
                _slider.value = _tmpCount;
            }
        }
    }
}
[System.Serializable]
public class IllustratedBookData
{
    [SerializeField, Header("キャラクターの名前")] private string _characterName;

    public string CharacterName => _characterName;
    [TextArea(1, 2)]
    [SerializeField, Header("キャラクターの待ち時間")] private string _characterTime;

    public string CharacterTime => _characterTime;

    [TextArea(1, 6)]
    [SerializeField, Header("キャラクターの説明")] private string _characterDetail;

    public string CharacterDetail => _characterDetail;
}
