using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 _touchDownPos;
    private Vector2 _touchUpPos;

    void Update()
    {
        if (GetTouchInfo() == TouchInfo.Began)
        {
            _touchDownPos = GetTouchPosition();
        }
    }

    public bool GetIsAttackRight(float offset)
    {
        if (GetUpSwipeLength() > offset)
        {
            if (GetTouchPosition().x > 360)
            {
                return true;
            }
        }

        return false;
    }

    public bool GetIsAttackLeft(float offset)
    {
        if (GetUpSwipeLength() > offset)
        {
            if (GetTouchPosition().x <= 360)
            {
                return true;
            }
        }

        return false;
    }

    public float GetUpAngle()
    {
        var angle = 0f;

        switch (GetTouchInfo())
        {
            case TouchInfo.Began:
                break;
            case TouchInfo.Moved:
                break;
            case TouchInfo.Ended:
                _touchUpPos = GetTouchPosition();
                angle = Mathf.Atan2((_touchUpPos - _touchDownPos).y, (_touchUpPos - _touchDownPos).x) * Mathf.Rad2Deg;
                break;
        }

        return angle;
    }

    public float GetSwipeLength()
    {
        var length = 0f;

        switch (GetTouchInfo())
        {
            case TouchInfo.Began:
                break;
            case TouchInfo.Moved:
                _touchUpPos = GetTouchPosition();
                length = (_touchUpPos - _touchDownPos).magnitude;
                break;
            case TouchInfo.Ended:
                _touchUpPos = GetTouchPosition();
                length = (_touchUpPos - _touchDownPos).magnitude;
                break;
        }

        return length;
    }

    public float GetUpSwipeLength()
    {
        var length = 0f;

        switch (GetTouchInfo())
        {
            case TouchInfo.Began:
                break;
            case TouchInfo.Moved:
                break;
            case TouchInfo.Ended:
                _touchUpPos = GetTouchPosition();
                length = (_touchUpPos - _touchDownPos).magnitude;
                break;
        }

        return length;
    }

    public Vector2 GetTouchPosition()
    {
        // Mouse
        TouchInfo touch = GetTouchInfo();
        if (touch != TouchInfo.None)
        {
            // �}�E�X���N���b�N
            var mousePosition = Vector2.zero;
            mousePosition.x = Input.mousePosition.x;
            mousePosition.y = Input.mousePosition.y;
            return mousePosition;
        }

        return Vector2.zero;
    }

    public Vector2 GetTouchUpPosition()
    {
        // Mouse
        TouchInfo touch = GetTouchInfo();
        if (touch == TouchInfo.Ended)
        {
            // �}�E�X���N���b�N
            var mousePosition = Vector2.zero;
            mousePosition.x = Input.mousePosition.x;
            mousePosition.y = Input.mousePosition.y;
            return mousePosition;
        }

        return Vector2.zero;
    }

    public TouchInfo GetTouchInfo()
    {
        if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
        if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
        if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }

        return TouchInfo.None;
    }

    public enum TouchInfo
    {
        /// <summary>
        /// �^�b�`�Ȃ�
        /// </summary>
        None = 99,

        // �ȉ��� UnityEngine.TouchPhase �̒l�ɑΉ�
        /// <summary>
        /// �^�b�`�J�n
        /// </summary>
        Began = 0,
        /// <summary>
        /// �^�b�`�ړ�
        /// </summary>
        Moved = 1,
        /// <summary>
        /// �^�b�`�Î~
        /// </summary>
        Stationary = 2,
        /// <summary>
        /// �^�b�`�I��
        /// </summary>
        Ended = 3,
        /// <summary>
        /// �^�b�`�L�����Z��
        /// </summary>
        Canceled = 4,
    }
}
