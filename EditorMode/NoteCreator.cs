using UnityEngine;
using UnityEditor;

public enum PadMapping
{
    A,
    B,
    X,
    Y
}

[ExecuteInEditMode]
public class NoteCreator : MonoBehaviour
{
    public GameObject NoteA;
    public GameObject NoteB;
    public GameObject NoteX;
    public GameObject NoteY;
    public string Name;
    public float EndTime;
    public PadMapping InputKey;
    [Range(1.0f, 10.0f)] public uint Speed;
    [Range(0.0f, 7.0f)] public uint PositionIndex;
    public bool CreateNote;

    public Vector3[] Positions { get { return positions; } }

    //private Object noteA;
    //private Object noteB;
    //private Object noteX;
    //private Object noteY;
    private Vector3[] positions;
    private Vector3 position;

    void Update()
    {
        if (CreateNote)
        {
            CreateNote = false;

            //if (noteA == null || noteB == null || noteX == null || noteY == null)
            //    FindPrefabs();
            PositionsGenerator();
            NoteGenerator();
        }
    }

    //private void FindPrefabs()
    //{
    //    noteA = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NoteA.prefab", typeof(GameObject));
    //    noteB = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NoteB.prefab", typeof(GameObject));
    //    noteX = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NoteX.prefab", typeof(GameObject));
    //    noteY = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NoteY.prefab", typeof(GameObject));
    //}

    public void PositionsGenerator()
    {
        float deltaA = 360.0f / GameManager.NUMBER_OF_SPAWN_POSITIONS;
        float currA = 0.0f;

        positions = new Vector3[GameManager.NUMBER_OF_SPAWN_POSITIONS];

        for (int i = 0; i < GameManager.NUMBER_OF_SPAWN_POSITIONS; i++)
        {
            float radiants = currA * Mathf.PI / 180.0f;
            float x = Mathf.Cos(radiants) * GameManager.SPAWN_DISTANCE_FROM_ORIGIN;
            float z = Mathf.Sin(radiants) * GameManager.SPAWN_DISTANCE_FROM_ORIGIN;
            currA += deltaA;

            positions[i] = new Vector3(x, 0.0f, z);
        }
    }

    private void NoteGenerator()
    {
        GameObject newNote = null;

        switch (InputKey)
        {
            case PadMapping.A:
                newNote = Instantiate((GameObject)NoteA, transform);
                break;
            case PadMapping.B:
                newNote = Instantiate((GameObject)NoteB, transform);
                break;
            case PadMapping.X:
                newNote = Instantiate((GameObject)NoteX, transform);
                break;
            case PadMapping.Y:
                newNote = Instantiate((GameObject)NoteY, transform);
                break;
            default:
                newNote = Instantiate((GameObject)NoteA, transform);
                break;
        }

        position = positions[PositionIndex];
        position = position.normalized * GameManager.SPAWN_DISTANCE_FROM_ORIGIN;

        newNote.name = Name;
        newNote.transform.localPosition = position;
        newNote.GetComponent<NoteBehaviour>().InputKey = InputKey;
        newNote.GetComponent<NoteBehaviour>().Speed = Speed;
        newNote.GetComponent<NoteBehaviour>().EndTime = EndTime;
        newNote.GetComponent<NoteBehaviour>().SpawnTime = Mathf.Clamp(EndTime - (GameManager.SPAWN_DISTANCE_FROM_ORIGIN / Speed), 0.0f, float.MaxValue);
        newNote.GetComponent<NoteBehaviour>().PositionIndex = PositionIndex;
        newNote.GetComponent<NoteBehaviour>().StartingPosition = position;
    }
}
