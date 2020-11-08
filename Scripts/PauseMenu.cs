using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public delegate void pause(bool active);
    public static event pause OnPause;
    public GameObject Crosshair;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                if(OnPause != null)
                {
                    OnPause(false);
                }
            }
            else
            {
                Pause();
                if (OnPause != null)
                {
                    OnPause(true);
                }
            }
        }
    }
    public void Resume ()
    {
        Crosshair.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        Crosshair.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Restart()
    {
        Debug.Log("LoadingMenu");
        SceneManager.LoadScene("Main Scene");
    }
    public void LoadMenu()
    {
        Debug.Log("LoadingMenu");
        Crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
       Time.timeScale = 1F;
        SceneManager.LoadScene("NewMainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
