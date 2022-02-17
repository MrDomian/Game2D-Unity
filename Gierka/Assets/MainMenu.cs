using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nextLevel;

    public void playGame()
    {
        SoundManager.PlaySound("clickMenu_m1");
        SceneManager.LoadScene(nextLevel);
    }

    public void quitGame()
    {
        SoundManager.PlaySound("clickMenu_m1");
        Application.Quit();
    }
}
