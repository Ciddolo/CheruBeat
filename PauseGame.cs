using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameDatas Data;
    public GameManager Manager;
    public GameObject PausePanel;
    public RestartGame restartScript;

    void Update()
    {
        if (Data.PlayGame && Input.GetButtonDown("Start"))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Restart()
    {
        PausePanel.SetActive(false);
        GameIsPaused = false;
        restartScript.ClickRestart();
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        Manager.AudioSource.Play();
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        Manager.AudioSource.Pause();
    }
}
