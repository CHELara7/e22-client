using UnityEngine;
using UnityEngine.UI;


public class ShowUIToggle : MonoBehaviour
{
    public enum Direction
    {
        up,
        right,
        down,
        left
    };

    [SerializeField] private Direction direction;
    [SerializeField] private Toggle toggle;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float moveValue;
    private Vector2 hidePos;

    public void SetPosition()
    {
        if (toggle.isOn)
        {
            hidePos = rectTransform.anchoredPosition - GetDirection(direction) * moveValue;
        }
        else
        {
            hidePos = rectTransform.anchoredPosition;
        }
    }

    private void Awake()
    {
        if (toggle.isOn)
        {
            hidePos = rectTransform.anchoredPosition - GetDirection(direction) * moveValue;
        }
        else
        {
            hidePos = rectTransform.anchoredPosition;
        }

        toggle.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                var showPos = hidePos + GetDirection(direction) * moveValue;
                rectTransform.anchoredPosition = showPos;
            }
            else
            {
                rectTransform.anchoredPosition = hidePos;
            }
        });
    }

    private Vector2 GetDirection(Direction direction)
    {
        int[] dirArray = { 0, 1, 0, -1 };
        return new Vector2(dirArray[(int)direction], dirArray[((int)direction + 1) % 4]);
    }
}
