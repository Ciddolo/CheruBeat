using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateGame : MonoBehaviour
{
    public Animator anim;
    public GameDatas Datas;
    public float GameDuration;
    public string NameOfPlayer;
    public float Score;

    bool setted = false;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        //Datas.NameOfPlayer = NameOfPlayer;
        //Datas.CurrentGameScore = Score;

        GameDuration -= Time.deltaTime;

        if(GameDuration <= 0&&setted == false)
        {
            setted = true;
            GameDuration = 0;
            Invoke("endGame", 3.0f);
            
        }
  


    }

    void endGame()
    {
        Datas.PlayGame = false;
        anim.SetTrigger("StatsComparisions");
        anim.SetTrigger("FadeInKnights");
    }
}
