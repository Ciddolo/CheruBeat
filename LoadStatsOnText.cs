using UnityEngine;
using TMPro;

public class LoadStatsOnText : MonoBehaviour
{
    /*
     * stats1: Score
     * stats2: Notes Catched
     * stats3: perchentageOfCatchedNotes
     * 
     * 
     */

    public TextMeshProUGUI[] Stats;
    public GameDatas data;

    bool loaded = false;

    void Update()
    {
        Stats[0].text = data.CurrentGameScore.ToString();
        Stats[1].text = data.NumberOfNotesCatched.ToString();
        Stats[2].text = data.PercentageOfCatchesNotes.ToString() + "%";
        Stats[3].text = ViewfinderTrigger.MaxStreak.ToString();
    }
}
