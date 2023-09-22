#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class DebugIaigiriPanel : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdown;
    [SerializeField] private SwipeAttack _swipeAttack;

    private const string IaigiriLabel = "Iaigiri";

    void Awake()
    {
        _dropdown.onValueChanged.AddListener(SetAttack);
        _dropdown.value = UnityEditor.EditorPrefs.GetInt(IaigiriLabel, 0);
    }

    private void SetAttack(int num)
    {
        _swipeAttack.SetAttackNum(num);
    }

    void OnDestroy()
    {
        UnityEditor.EditorPrefs.SetInt(IaigiriLabel, _dropdown.value);
    }
}
#endif
