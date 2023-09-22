using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleCanvasManager : MonoBehaviour
{
    [SerializeField] private Button nextSceneToggle;
    void Awake() 
    {
        UISetUp();
        
    }

    private void UISetUp() 
    {
        nextSceneToggle.onClick.AddListener(GameSceneManager.ToMainMenu);
    }

}
