using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEndGame : MonoBehaviour
{
    public GameObject EndCanvas;
    public GameObject InvalidGameOverText;

    public GameDatas Data;

    public float MinScoreToWin;
    bool loaded= false;

    void Start()
    {
        SceneManager.sceneLoaded += PlayGame;
    }

    private void PlayGame(Scene arg0, LoadSceneMode arg1)
    {
        Data.PlayGame = true;
    }
}
