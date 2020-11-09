using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    SceneHandler sceneHandler;
    public GameDatas gameDatas;
    public GameManager manager;
    

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    public void ClickRestart()
    {
        Time.timeScale = 1f;
        GameManager.CurrentTime = 0f;
        ViewfinderTrigger.ResetStreak();
        

        manager.AudioSource.Stop();
        sceneHandler.LoadGameScene();
    }    
}
