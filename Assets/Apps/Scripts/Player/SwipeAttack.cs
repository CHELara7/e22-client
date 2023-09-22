using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    [SerializeField] private SphereCollider _iaiCollider;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _startIaiTime;
    [SerializeField] private float _addScale;
    [SerializeField] private float _swipeOffset;
    [SerializeField] private PlayerController _playerController;

    private int _iaiAttackNum;
    private float _time;
    private bool _allowAttack;

    void Update()
    {
        Harai();

        switch (_iaiAttackNum)
        {
            case 0:
                Iai1();
                break;
            case 1: 
                Iai2(); 
                break;
            case 2:
                Iai3();
                break;
        }
    }

    // 後々はらいといあいはコード連結させる
    private void Harai()
    {
        switch (_inputManager.GetTouchInfo())
        {
            case InputManager.TouchInfo.Began:
                break;
            case InputManager.TouchInfo.Moved:
                _time += Time.deltaTime;
                break;
            case InputManager.TouchInfo.Ended:
                if (_time < _startIaiTime)
                {
                    if (_inputManager.GetUpSwipeLength() > _swipeOffset)
                    {
                        HaraiAttack();
                    }
                }
                break;
        }
    }

    // おさえる→スライド→はなす
    private void Iai1()
    {
        switch (_inputManager.GetTouchInfo())
        {
            case InputManager.TouchInfo.Began:
                break;
            case InputManager.TouchInfo.Moved:
                _time += Time.deltaTime;
                if (_time >= _startIaiTime)
                {
                    if (_inputManager.GetSwipeLength() < _swipeOffset)
                    {
                        return;
                    }

                    _iaiCollider.radius += _addScale;

                    if (!_allowAttack)
                    {
                        _allowAttack = true;
                    }
                }
                break;
            case InputManager.TouchInfo.Ended:
                IaiAttack();
                IaiReset();
                break;
        }
    }

    // おさえる→はなす
    private void Iai2()
    {
        switch (_inputManager.GetTouchInfo())
        {
            case InputManager.TouchInfo.Began:
                break;
            case InputManager.TouchInfo.Moved:
                _time += Time.deltaTime;
                if (_time >= _startIaiTime)
                {
                    _iaiCollider.radius += _addScale;

                    if (!_allowAttack)
                    {
                        _allowAttack = true;
                    }
                }
                break;
            case InputManager.TouchInfo.Ended:
                if (_inputManager.GetUpSwipeLength() < _swipeOffset)
                {
                    IaiAttack();
                }

                IaiReset();
                break;
        }
    }

    // おさえる→スライド
    private void Iai3()
    {
        switch (_inputManager.GetTouchInfo())
        {
            case InputManager.TouchInfo.Began:
                break;
            case InputManager.TouchInfo.Moved:
                _time += Time.deltaTime;
                if (_time >= _startIaiTime)
                {
                    _iaiCollider.radius += _addScale;

                    if (!_allowAttack)
                    {
                        _allowAttack = true;
                    }
                }
                break;
            case InputManager.TouchInfo.Ended:
                if (_inputManager.GetUpSwipeLength() > _swipeOffset)
                {
                    IaiAttack();
                }

                IaiReset();
                break;
        }
    }

    private void HaraiAttack()
    {
        if (Mathf.Abs(_inputManager.GetUpAngle()) < 90)
        {
            _playerController.AttackRight();
        }
        else
        {
            _playerController.AttackLeft();
        }

        _time = 0f;
    }

    private void IaiAttack()
    {
        if (_allowAttack)
        {
            // Todo Attack
            Debug.Log("居合切り");
        }
    }

    private void IaiReset()
    {
        _time = 0f;
        _allowAttack = false;
        _iaiCollider.radius = 0.1f;
    }

    public void SetAttackNum(int num)
    {
        _iaiAttackNum = num;
    }
}
