using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneHandler : MonoBehaviour
{
    public GameDatas gameDatas;
    public AsyncOperation operation;
    Animator animator;

    bool Invoked = false;

    public bool PrintLederboard;

    private void Start()
    {
        gameDatas.Initialize();

        DontDestroyOnLoad(gameObject);

        animator = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();

        SceneManager.activeSceneChanged += PickAnimator;
    }

    private void Update()
    {
        if (operation != null && operation.progress == 0.9f && !Invoked)
        {
            StartFade();
        }
        if (PrintLederboard)
        {
            PrintLederboard = false;
        }
    }

    #region //LoadOfScenes
    public void LoadContextScene()
    {
        operation = SceneManager.LoadSceneAsync("ContextScene", LoadSceneMode.Single);
        operation.allowSceneActivation = false;
    }

    public void LoadStartScene()
    {
        operation = SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
        operation.allowSceneActivation = false;
    }

    public void LoadGameScene()
    {
        operation = SceneManager.LoadSceneAsync("GamePlayScene", LoadSceneMode.Single);
        operation.allowSceneActivation = false;
    }

    public void LoadCreditsScene()
    {
        operation = SceneManager.LoadSceneAsync("CreditsScene", LoadSceneMode.Single);
        operation.allowSceneActivation = false;
    }
    #endregion

    #region //AnimationStuffs
    void StartFade()
    {
        animator.SetTrigger("FadeOutPanel");
        Invoked = true;


        Invoke("ChangeScene", 1.1f);
    }

    void ChangeScene()
    {
        operation.allowSceneActivation = true;
        operation = null;
        Invoked = false;
    }

    void PickAnimator(Scene current, Scene next)
    {
        animator = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }
    #endregion

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
