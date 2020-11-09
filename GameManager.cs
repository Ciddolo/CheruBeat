using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public class GameManager : MonoBehaviour
{
    public const uint NUMBER_OF_SPAWN_POSITIONS = 8;
    public const float SPAWN_DISTANCE_FROM_ORIGIN = 8.0f;
    public const float THUMBSTICK_MULTIPLIER = 2.0f;
    public const float THUMBSTICK_THRESHOLD = 0.1f;
    public const float NOTE_DESPAWN_SPEED = 10.0f;
    public const float HIT_TEXT_SHOW_SPEED = 5.0f;
    public const float HIT_TEXT_HIDE_SPEED = 10.0f;

    public AudioSource AudioSource;
    private Transform NotesParent;
    public float StartTime;
    [Range(0.0f, 20.0f)] public uint Delay;
    [Range(0.1f, 1.0f)] public float TimeScale;

    public static float CurrentTime;

    private Queue<GameObject> notesQueue;
    private Queue<float> spawnTimeQueue;
    private float nextNoteSpawnTime;
    private bool isStarted;
    public GameDatas datas;

    public Transform HardModeParent;
    public Transform EasyModeParent;

    void Start()
    {
        CheckGameMode(datas.GameModeHard);
        notesQueue = new Queue<GameObject>();
        spawnTimeQueue = new Queue<float>();
        AudioSource = gameObject.GetComponent<AudioSource>();

        if (NotesParent.transform.childCount > 0)
        {
            for (int i = 0; i < NotesParent.childCount; i++)
            {
                GameObject currentNote = NotesParent.GetChild(i).gameObject;
                currentNote.SetActive(false);
                notesQueue.Enqueue(currentNote);
                spawnTimeQueue.Enqueue(currentNote.GetComponent<NoteBehaviour>().SpawnTime);
            }
        }

        if (spawnTimeQueue.Count > 0)
            nextNoteSpawnTime = spawnTimeQueue.Dequeue();

        CurrentTime += StartTime;
    }

    void Update()
    {
        //Debug.Log("TIMER:[" + CurrentTime + "] AUDIO:[" + audioSource.time + "]");

        //Time.timeScale = TimeScale;
        //audioSource.pitch = TimeScale;
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= Delay)
        {
            if (!AudioSource.isPlaying && !isStarted)
            {
                if (CurrentTime - Delay < AudioSource.clip.length)
                {
                    if (Delay <= 0.0f)
                        AudioSource.time = CurrentTime;
                    else
                        AudioSource.time = CurrentTime - Delay;

                    AudioSource.Play();
                    isStarted = true;
                }
            }
        }

        if (CurrentTime >= nextNoteSpawnTime)
        {
            if (notesQueue.Count > 0)
            {
                SpawnNote(notesQueue.Dequeue());
                if (spawnTimeQueue.Count > 0)
                    nextNoteSpawnTime = spawnTimeQueue.Dequeue();
                else
                    nextNoteSpawnTime = -1.0f;
            }
        }
    }

    public static void SpawnNote(GameObject note)
    {
        note.GetComponent<NoteBehaviour>().Direction = Vector3.zero - note.transform.position;
        note.SetActive(true);
    }

    public void DisableNote(GameObject note)
    {
        note.SetActive(false);
        //notesQueue.Enqueue(note);
    }

    public void CheckGameMode(bool mode)
    {
        if (mode)
            NotesParent = HardModeParent;
        else
            NotesParent = EasyModeParent;

        datas.SetNumberOfNotes(NotesParent.childCount);
    }

    public int GetNotesNumber(Difficulty difficulty)
    {
        int n;
        switch (difficulty)
        {
            case Difficulty.Easy:
                n = EasyModeParent.childCount;
                break;
            case Difficulty.Hard:
                n = HardModeParent.childCount;
                break;
            default:
                n = -1;
                break;
        }
        return n;
    }
}
