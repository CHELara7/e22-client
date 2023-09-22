using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    [SerializeField] private int _buttonSize;
    [SerializeField] private float _screenSwipeLength;
    [SerializeField] private GameObject _shopObj;
    [SerializeField] private GameObject _equipObj;
    [SerializeField] private GameObject _battleObj;
    [SerializeField] private GameObject _missionObj;
    [SerializeField] private GameObject _skillObj;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _battleButton;
    [SerializeField] private Button _missionButton;
    [SerializeField] private Button _skillButton;
    [SerializeField] private RectTransform _shopRec;
    [SerializeField] private RectTransform _equipRec;
    [SerializeField] private RectTransform _battleRec;
    [SerializeField] private RectTransform _missionRec;
    [SerializeField] private RectTransform _skillRec;
    [SerializeField] private Image _shopImage;
    [SerializeField] private Image _equipImage;
    [SerializeField] private Image _battleImage;
    [SerializeField] private Image _missionImage;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Color32 _selectedColor;
    [SerializeField] private Color32 _nonSelectedColor;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Button _normalBattleButton;

    private int _halfButtonSize;
    private Vector2 _selectSize;
    private Vector2 _nonSelectSize;
    private Mode _currentMode;

    void Awake()
    {
        _halfButtonSize = _buttonSize / 2;
        _selectSize = new Vector2(_buttonSize * 2, _buttonSize);
        _nonSelectSize = new Vector2(_buttonSize, _buttonSize);

        _shopButton.onClick.AddListener(SelectShop);
        _equipButton.onClick.AddListener(SelectEquip);
        _battleButton.onClick.AddListener(SelectBattle);
        _missionButton.onClick.AddListener(SelectMission);
        _skillButton.onClick.AddListener(SelectSkill);
        _normalBattleButton.onClick.AddListener(GameSceneManager.ToNormalBattle);

        // 最初はバトル選択画面
        SelectBattle();
    }

    void Update()
    {
        var length = _inputManager.GetUpSwipeLength();
        if (length > _screenSwipeLength)
        {
            var angle = _inputManager.GetUpAngle();

            // 右にスワイプ
            if (Math.Abs(angle) > 90)
            {
                if ((int)_currentMode < (int)Mode.Skill)
                {
                    SwipeModeSelect(true);
                }
            }
            // 左にスワイプ
            else
            {
                if ((int)_currentMode > (int)Mode.Shop)
                {
                    SwipeModeSelect(false);
                }
            }
        }
    }

    private void SelectShop()
    {
        _currentMode = Mode.Shop;
        SetSelect(_shopObj, _shopRec, _shopImage);
        SetPosX(-_buttonSize * 2, -_halfButtonSize, _halfButtonSize, _buttonSize + _halfButtonSize,
            _buttonSize * 2 + _halfButtonSize);
    }
    private void SelectEquip()
    {
        _currentMode = Mode.Equip;
        SetSelect(_equipObj, _equipRec, _equipImage);
        SetPosX(-_buttonSize * 2 - _halfButtonSize, -_buttonSize, _halfButtonSize, _buttonSize + _halfButtonSize,
            _buttonSize * 2 + _halfButtonSize);
    }
    private void SelectBattle()
    {
        _currentMode = Mode.Battle;
        SetSelect(_battleObj, _battleRec, _battleImage);
        SetPosX(-_buttonSize * 2 - _halfButtonSize, -_buttonSize - _halfButtonSize, 0, _buttonSize + _halfButtonSize,
            _buttonSize * 2 + _halfButtonSize);
    }
    private void SelectMission()
    {
        _currentMode = Mode.Mission;
        SetSelect(_missionObj, _missionRec, _missionImage);
        SetPosX(-_buttonSize * 2 - _halfButtonSize, -_buttonSize - _halfButtonSize, -_halfButtonSize, _buttonSize,
            _buttonSize * 2 + _halfButtonSize);
    }
    private void SelectSkill()
    {
        _currentMode = Mode.Skill;
        SetSelect(_skillObj, _skillRec, _skillImage);
        SetPosX(-_buttonSize * 2 - _halfButtonSize, -_buttonSize - _halfButtonSize, -_halfButtonSize, _halfButtonSize,
            _buttonSize * 2);
    }

    /// <summary>
    /// 右スワイプの時true
    /// </summary>
    /// <param name="isRight"></param>
    private void SwipeModeSelect(bool isRight)
    {
        switch (_currentMode)
        {
            case Mode.Shop:
                SelectEquip();
                break;
            case Mode.Equip:
                if (isRight)
                {
                    SelectBattle();
                }
                else
                {
                    SelectShop();
                }
                break;
            case Mode.Battle:
                if (isRight)
                {
                    SelectMission();
                }
                else
                {
                    SelectEquip();
                }
                break;
            case Mode.Mission:
                if (isRight)
                {
                    SelectSkill();
                }
                else
                {
                    SelectBattle();
                }
                break;
            case Mode.Skill:
                SelectMission();
                break;
        }
    }

    private void SetSelect(GameObject obj, RectTransform rect, Image image)
    {
        _shopObj.SetActive(false);
        _equipObj.SetActive(false);
        _battleObj.SetActive(false);
        _missionObj.SetActive(false);
        _skillObj.SetActive(false);

        _shopRec.sizeDelta = _nonSelectSize;
        _equipRec.sizeDelta = _nonSelectSize;
        _battleRec.sizeDelta = _nonSelectSize;
        _missionRec.sizeDelta = _nonSelectSize;
        _skillRec.sizeDelta = _nonSelectSize;

        _shopImage.color = _nonSelectedColor;
        _equipImage.color = _nonSelectedColor;
        _battleImage.color = _nonSelectedColor;
        _missionImage.color = _nonSelectedColor;
        _skillImage.color = _nonSelectedColor;

        obj.SetActive(true);
        rect.sizeDelta = _selectSize;
        image.color = _selectedColor;
    }

    private void SetPosX(int shopPosX, int equipPosX, int battlePosX, int missionPosX, int skillPosX)
    {
        var shopPos = _shopRec.localPosition;
        shopPos.x = shopPosX;
        _shopRec.localPosition = shopPos;

        var equipPos = _equipRec.localPosition;
        equipPos.x = equipPosX;
        _equipRec.localPosition = equipPos;

        var battlePos = _battleRec.localPosition;
        battlePos.x = battlePosX;
        _battleRec.localPosition = battlePos;

        var missionPos = _missionRec.localPosition;
        missionPos.x = missionPosX;
        _missionRec.localPosition = missionPos;

        var skillPos = _skillRec.localPosition;
        skillPos.x = skillPosX;
        _skillRec.localPosition = skillPos;
    }

    private enum Mode
    {
        Shop,
        Equip,
        Battle,
        Mission,
        Skill
    }
}
