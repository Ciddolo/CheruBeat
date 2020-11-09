using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RefreshLederBoard : MonoBehaviour
{
    //aggiunge ai Text della liberboard i dati di gioco
    public GameDatas Datas;
    Player[] currLederboard;

    public TextMeshProUGUI[] NameTexts;
    public TextMeshProUGUI[] ScoreTexts;

    bool IsEnded = true;

    private void Update()
    {
        if(!Datas.PlayGame && IsEnded)
        {
            IsEnded = false;
            
            if (Datas.GameModeHard)
            {
                currLederboard = Datas.LederboardsGame2;
            }
            else
            {
                currLederboard = Datas.LederboardsGame1;
            }

            Datas.CheckRecordInLederboard(currLederboard, Datas.CurrentGameScore, Datas.NameOfPlayer);
            Refresh(currLederboard);
        }
    }
    
    void Refresh(Player[] currLederboard)
    {
        for (int i = 0; i < currLederboard.Length - 1; i++)
        {
            for (int j = i + 1; j < currLederboard.Length; j++)
            {
                if (currLederboard[i].Score < currLederboard[j].Score)
                {
                    Player temp = currLederboard[i];
                    currLederboard[i] = currLederboard[j];
                    currLederboard[j] = temp;
                }
            }
        }

        for (int i = 0; i < currLederboard.Length; i++)
        {
            NameTexts[i].text = currLederboard[i].Name;

            if (currLederboard[i].Score == 0)
            {
                continue;
            }
            else
            {
                ScoreTexts[i].text = currLederboard[i].Score.ToString();
            }
        }        
    }
}
