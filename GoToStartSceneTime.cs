using UnityEngine;

public class GoToStartSceneTime : MonoBehaviour
{
    SceneHandler sceneHandler;
    public float time;
    bool go = true;

    private void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneHandler>();
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0 && go == true)
        {
            go = false;
            goToStartScene();
        }
    }

    public void goToStartScene()
    {
        sceneHandler.LoadStartScene();
    }
}
