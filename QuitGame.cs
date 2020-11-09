using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    SceneHandler sceneHandler;
    public GameDatas gameDatas;

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    public void ClickQuit()
    {
        Time.timeScale = 1f;
        //gameDatas.IsInMenu = true;
        sceneHandler.LoadStartScene();
    }
}
