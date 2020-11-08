using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour
{
    public void PlayGameZombie()
    {
        SceneManager.LoadScene("Zombie Scene");
    }
    public void PlayGameAI()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void OldMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
