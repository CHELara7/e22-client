using UnityEngine;

public class TimeScale : MonoBehaviour
{
    private const float MinValue = 0.00000001f; // 0.0fにするとUnityのUpdate Event系で呼ばれなくなる関数があるので極小値を定義する
    [SerializeField] private float _scale = 1.0f;
    private static TimeScale instance;
    public static TimeScale Instance 
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject { name = "[TimeScale]" };
                instance = go.AddComponent<TimeScale>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    public float Scale
    {
        get => _scale;
        set
        {
            if (value < MinValue)
            {
                value = MinValue;
            }
            if (value > 100.0f)
            {
                value = 100.0f;
            }
            _scale = value;
        }
    }

    public bool IsPause;
    public bool IsSkip;
    void Update()
    {
        if (IsPause)
        {
            if (IsSkip)
            {
                Time.timeScale = Scale;
                IsSkip = false;
            }
            else
            {
                Time.timeScale = MinValue;
            }
        }
        else
        {
            Time.timeScale = Scale;
        }
    }
}