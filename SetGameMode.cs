using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameMode : MonoBehaviour
{
    SceneHandler sceneHandler;
    public GameDatas gameDatas;

    void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    public void ClickToLoadNormalMode()
    {
        gameDatas.GameModeHard = false;
        gameDatas.IsInMenu = false;

        GameManager.CurrentTime = 0f;
        ViewfinderTrigger.ResetStreak();

        sceneHandler.LoadGameScene();
    }

    public void ClickToLoadHardMode()
    {
        gameDatas.GameModeHard = true;
        gameDatas.IsInMenu = false;

        GameManager.CurrentTime = 0f;
        ViewfinderTrigger.ResetStreak();

        sceneHandler.LoadGameScene();
    }
}
