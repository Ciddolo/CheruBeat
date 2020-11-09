using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class InsertName : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject QuitButton;


    public TextMeshProUGUI text;
    SceneHandler sceneHandler;
    public GameDatas gameDatas;

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    public void OnSelect()
    {
        gameDatas.NameOfPlayer = text.text;
        sceneHandler.LoadContextScene();


    }

    public void OnClickStart()
    {
        gameObject.SetActive(true);
        StartButton.SetActive(false);
        QuitButton.SetActive(false);

    }
}
