using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextLevel : MonoBehaviour
{
    SceneHandler sceneHandler;
    public GameDatas gameDatas;

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    public void NextLevel()
    {
        Invoke("AudioMethod", 1f);
        sceneHandler.LoadCreditsScene();
    }

    void AudioMethod()
    {
        gameDatas.IsInMenu = true;
        gameDatas.IsSettedMusic = true;
    }
}
