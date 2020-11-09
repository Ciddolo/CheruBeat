using UnityEngine;
using TMPro;

public class NoteBehaviour : MonoBehaviour
{
    public Vector3 StartingPosition { get; set; }
    public PadMapping InputKey { get { return inputKey; } set { inputKey = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
    public float SpawnTime { get { return spawnTime; } set { spawnTime = value; } }
    public float EndTime { get { return endTime; } set { endTime = value; } }
    public uint PositionIndex { get { return positionIndex; } set { positionIndex = value; } }
    public Vector3 Direction { get; set; }

    private GameManager manager;
    private TextMeshPro text;
    private RectTransform rectText;
    [SerializeField] private float speed;
    [SerializeField] private float spawnTime;
    [SerializeField] private float endTime;
    [SerializeField] private PadMapping inputKey;
    [SerializeField] private uint positionIndex;
    private bool isDespawning;
    private bool isShowingText;
    private bool isHiddenText;
    private bool isEspanding;
    private bool isFading;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = transform.GetChild(1).GetComponent<TextMeshPro>();
        rectText = transform.GetChild(1).GetComponent<RectTransform>();
    }

    void Update()
    {
        MoveBehaviour();

        if (isShowingText)
            ShowText();
        if (isHiddenText)
            HideText();
        if (isDespawning)
            Despawn();
    }

    private void Despawn()
    {
        transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale, Vector3.zero, Time.deltaTime * GameManager.NOTE_DESPAWN_SPEED);
        if (isEspanding)
            transform.GetChild(2).localScale = Vector3.Lerp(transform.GetChild(2).localScale, new Vector3(80.0f, 80.0f, 80.0f), Time.deltaTime * GameManager.NOTE_DESPAWN_SPEED * 2.0f);
        if (transform.GetChild(2).localScale.x >= 79.9)
        {
            isEspanding = false;
            isFading = true;
        }
        if (isFading)
            transform.GetChild(2).localScale = Vector3.Lerp(transform.GetChild(2).localScale, new Vector3(0.0f, 0.0f, 0.0f), Time.deltaTime * GameManager.NOTE_DESPAWN_SPEED);
        if (transform.GetChild(0).localScale.x <= 0.1f)
            EnqueueNote();
    }

    private void MoveBehaviour()
    {
        transform.position += Direction.normalized * Time.deltaTime * speed;

        if ((Vector3.zero - transform.position).magnitude < 0.1f && !isDespawning)
            Missed();
    }

    private void ShowText()
    {
        rectText.localScale = Vector3.Lerp(rectText.localScale, new Vector3(1.5f, 1.5f, 1.5f), Time.deltaTime * GameManager.HIT_TEXT_SHOW_SPEED);

        if (1.5f - rectText.localScale.x <= 0.1f)
        {
            rectText.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            isShowingText = false;
            isHiddenText = true;
        }
    }

    private void HideText()
    {
        rectText.localScale = Vector3.Lerp(rectText.localScale, Vector3.zero, Time.deltaTime * GameManager.HIT_TEXT_HIDE_SPEED);

        if (rectText.localScale.x <= 0.1f)
        {
            rectText.localScale = Vector3.zero;
            isHiddenText = false;
        }
    }

    private void Missed()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        text.color = Color.grey;
        text.text = HitType.Miss.ToString().ToUpper();
        Direction = Vector3.zero;
        isShowingText = true;
        isDespawning = true;
        isFading = true;
        ViewfinderTrigger.ResetStreak();
    }

    public void Hit(string hitText, Color color)
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        text.color = color;
        text.text = hitText.ToUpper();
        Direction = Vector3.zero;
        isShowingText = true;
        isDespawning = true;
        isEspanding = true;
    }

    public void EnqueueNote()
    {
        ResetNote();
        manager.DisableNote(gameObject);
    }

    public void ResetNote()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
        isDespawning = false;
        isShowingText = false;
        isHiddenText = false;
        isEspanding = false;
        isFading = false;
        Direction = Vector3.zero - StartingPosition;
        transform.position = StartingPosition;
        transform.GetChild(0).localScale = Vector3.one;
        rectText.localScale = Vector3.zero;
    }
}
