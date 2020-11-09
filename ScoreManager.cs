using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Streak;
    public TextMeshProUGUI Percent;
    public GameDatas Datas;
    private float percent;

    void Start()
    {
        ResetScore();
        Score.text = "Score: " + Datas.CurrentGameScore.ToString("00000");
        Streak.text = "Streak: " + Datas.CurrentGameScore;
        Percent.text = " ";
    }

    void Update()
    {
        Score.text = "Score: " + Datas.CurrentGameScore.ToString("00000");
        Streak.text = "Streak: " + ViewfinderTrigger.CurrentStreak;
        percent = Mathf.Clamp((Datas.NumberOfNotesCatched * 100) / Datas.NumberOfNotes, 0.0f, 100.0f);
        Datas.PercentageOfCatchesNotes = (int)percent;
        Percent.text = (int)percent + "%";
    }

    public void AddScore(float points)
    {
        if (points > 0.0f)
            Datas.NumberOfNotesCatched++;
        Datas.CurrentGameScore += points;
        if (Datas.CurrentGameScore < 0.0f)
            Datas.CurrentGameScore = 0.0f;
    }

    public void ResetScore()
    {
        Datas.NumberOfNotesCatched = 0;
        Datas.CurrentGameScore = 0.0f;
        percent = 0.0f;
    }
}
