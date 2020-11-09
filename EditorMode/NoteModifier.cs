using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(NoteBehaviour))]
public class NoteModifier : MonoBehaviour
{
    public bool EditSpeed;
    [Range(1.0f, 10.0f)] public uint NewSpeed;
    public bool EditEndTime;
    public float NewEndTime;
    public bool EditPositionIndex;
    [Range(0.0f, 7.0f)] public uint NewPositionIndex;
    public bool SaveChanges;

    void Update()
    {
        if (!SaveChanges)
            return;

        SaveChanges = false;

        NoteBehaviour note = GetComponent<NoteBehaviour>();
        NoteCreator creator = note.transform.parent.GetComponent<NoteCreator>();

        if (EditSpeed)
        {
            note.Speed = NewSpeed;
            note.SpawnTime = Mathf.Clamp(note.EndTime - (GameManager.SPAWN_DISTANCE_FROM_ORIGIN / note.Speed), 0.0f, float.MaxValue);
        }

        if (EditEndTime)
        {
            note.SpawnTime = Mathf.Clamp(NewEndTime - (GameManager.SPAWN_DISTANCE_FROM_ORIGIN / note.Speed), 0.0f, float.MaxValue);
            note.EndTime = NewEndTime;
        }

        if (EditPositionIndex)
        {
            creator.PositionsGenerator();
            note.StartingPosition = creator.Positions[NewPositionIndex];
            note.PositionIndex = NewPositionIndex;
        }
    }
}
