using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUiRootObject;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TextMeshProUGUI _pauseTMPro;
    [SerializeField] private Button _endGameButton;

    private const string PAUSE = "Pause";
    private const string PLAYBACK = "PlayBack";

    private void Awake()
    {
        PauseUISetUp();
    }

    private void PauseUISetUp()
    {
        _pauseButton.onClick.AddListener(Pause);
        _endGameButton.onClick.AddListener(EndGame);
    }

    private void Pause()
    {
        if (!TimeScale.Instance.IsPause)
        {
            _pauseTMPro.text = PLAYBACK;
            _pauseUiRootObject.SetActive(true);
            TimeScale.Instance.IsPause = true;
        }
        else
        {
            _pauseTMPro.text = PAUSE;
            _pauseUiRootObject.SetActive(false);
            TimeScale.Instance.IsPause = false;
        }
    }

    private void EndGame() 
    {
        GameSceneManager.ToMainMenu();
        TimeScale.Instance.IsPause = false;
    }
}
