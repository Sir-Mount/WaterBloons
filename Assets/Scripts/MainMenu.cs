using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Object _playScene;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(_playScene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
