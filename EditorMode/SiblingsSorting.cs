using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class SiblingsSorting : MonoBehaviour
{
    public TMP_FontAsset NewFont;
    public bool SortByEndTime;
    public bool GetNumberOfNotes;
    public bool ChangeFont;

    private int numberOfSiblings;

    void Update()
    {
        if (ChangeFont)
        {
            ChangeFont = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(1).GetComponent<TextMeshPro>().font = NewFont;
            }
        }

        if (SortByEndTime)
        {
            SortByEndTime = false;

            numberOfSiblings = transform.childCount;
            BubbleSort();
        }

        if (GetNumberOfNotes)
        {
            GetNumberOfNotes = false;

            Debug.Log(transform.childCount);
        }
    }

    private void BubbleSort()
    {
        if (numberOfSiblings < 2)
            return;

        for (int i = 0; i < numberOfSiblings; i++)
        {
            for (int j = 0; j < numberOfSiblings - 1; j++)
            {
                if (transform.GetChild(j).GetComponent<NoteBehaviour>().EndTime > transform.GetChild(j + 1).GetComponent<NoteBehaviour>().EndTime)
                    transform.GetChild(j + 1).SetSiblingIndex(j);
            }
        }
    }

}
