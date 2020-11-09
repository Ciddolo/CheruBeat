using UnityEngine;
using TMPro;

public class CheckLederboardName : MonoBehaviour
{
    public GameDatas Datas;
    public TextMeshProUGUI Text;

    void Start()
    {
        if (Datas.GameModeHard)
        {
            Text.text = "Lederboard Hard";
        }
        else
        {
            Text.text = "Lederboard Easy";
        }
    }
}
