using UnityEngine;
using TMPro;

public enum HitType
{
    Miss,
    Poor,
    Good,
    Perfect
}

public class ViewfinderTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;

    [Range(0.0f, 1.0f)]
    public float PerfectThreshold;
    [Range(0.0f, 1.0f)]
    public float GoodThreshold;
    [Range(0.0f, 1.0f)]
    public float PoorThreshold;

    public GameObject CurrentNote { get; set; }
    public bool IsThumbStickMoving { get; set; }

    private const uint POINTS_MULTIPLIER = 2;

    private float points;
    private float malus;
    public static uint CurrentStreak;
    public static uint MaxStreak;

    void Start()
    {
        malus = -25.0f;
    }

    void Update()
    {
        if (Input.GetButtonDown("A") || Input.GetButtonDown("B") || Input.GetButtonDown("X") || Input.GetButtonDown("Y"))
        {
            if (CurrentNote == null)
            {
                //scoreManager.AddScore(malus);
                Debug.Log("asd");
                ResetStreak();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CurrentNote = other.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        if (!IsThumbStickMoving)
            return;

        if (CurrentNote == null)
            CurrentNote = other.gameObject;

        if (CurrentNote.tag == "NoteA" && Input.GetButtonDown("A"))
            Hit(CurrentNote.GetComponent<NoteBehaviour>());
        else if (CurrentNote.tag == "NoteB" && Input.GetButtonDown("B"))
            Hit(CurrentNote.GetComponent<NoteBehaviour>());
        else if (CurrentNote.tag == "NoteX" && Input.GetButtonDown("X"))
            Hit(CurrentNote.GetComponent<NoteBehaviour>());
        else if (CurrentNote.tag == "NoteY" && Input.GetButtonDown("Y"))
            Hit(CurrentNote.GetComponent<NoteBehaviour>());
    }

    void OnTriggerExit()
    {
        CurrentNote = null;
    }

    public static void ResetStreak()
    {
        if (CurrentStreak > MaxStreak)
            MaxStreak = CurrentStreak;
        CurrentStreak = 0;
    }

    private void Hit(NoteBehaviour note)
    {
        HitType type = CalculateHitType(transform, note.transform);
        Color color;
        switch (type)
        {
            case HitType.Miss:
                color = Color.grey;
                break;
            case HitType.Poor:
                color = Color.yellow;
                break;
            case HitType.Good:
                color = Color.green;
                break;
            case HitType.Perfect:
                color = Color.magenta;
                break;
            default:
                color = Color.black;
                break;
        }
        points = 50.0f * (int)type * POINTS_MULTIPLIER;
        scoreManager.AddScore(points);
        note.Hit(type.ToString(), color);
        CurrentStreak++;
    }

    private HitType CalculateHitType(Transform viewfinder, Transform note)
    {
        float distance = (viewfinder.position - note.position).magnitude;
        if (distance <= PerfectThreshold)
            return HitType.Perfect;
        else if (distance <= GoodThreshold)
            return HitType.Good;
        else
            return HitType.Poor;
    }
}
