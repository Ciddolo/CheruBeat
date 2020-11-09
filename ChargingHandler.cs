using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChargingHandler : MonoBehaviour
{
    SceneHandler sceneHandler;
    public Image image;

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
    }

    void Update()
    {
        if (sceneHandler.operation != null)
        {
            image.fillAmount = sceneHandler.operation.progress;
        }
        else
        {
            sceneHandler.operation = null;
        }
    }
}
