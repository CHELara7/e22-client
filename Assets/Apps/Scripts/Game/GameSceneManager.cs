using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    enum SceneEnum
    {
        Title = 0,
        MainMenu,
        NormalBattle,
        EndlessBattle,
    }

    static private IEnumerator NextLoadSceneAsync(string sceneName) 
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    static private void NextLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    static public void ToTitle() 
    {
        NextLoadScene(SceneEnum.Title.ToString());
    }

    static public void ToMainMenu() 
    {
        NextLoadScene(SceneEnum.MainMenu.ToString());
    }

    static public void ToNormalBattle() 
    {
        NextLoadScene(SceneEnum.NormalBattle.ToString());
    }

    static public void ToEndlessBattle()
    {
        NextLoadScene(SceneEnum.EndlessBattle.ToString());
    }
}
