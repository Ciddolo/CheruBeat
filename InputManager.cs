using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Material RingIdleMaterial;
    public Material RingPressedMaterial;

    private Vector3[] positions;
    private GameObject ring;
    private Transform viewfinder;
    private ViewfinderTrigger viewfinderTrigger;
    private Vector3 thumbStickPosition;
    private bool isThumbStickMoving;
    private float thumbStickMagnitude;
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    public float viewfinderSpeed;

    void Start()
    {
        ring = transform.GetChild(0).gameObject;
        viewfinder = transform.GetChild(1);
        viewfinderTrigger = viewfinder.GetComponent<ViewfinderTrigger>();

        float deltaA = 360.0f / GameManager.NUMBER_OF_SPAWN_POSITIONS;
        float currA = 0.0f;

        positions = new Vector3[GameManager.NUMBER_OF_SPAWN_POSITIONS];

        for (int i = 0; i < GameManager.NUMBER_OF_SPAWN_POSITIONS; i++)
        {
            float radiants = currA * Mathf.PI / 180f;
            float x = Mathf.Cos(radiants) * GameManager.THUMBSTICK_MULTIPLIER;
            float z = Mathf.Sin(radiants) * GameManager.THUMBSTICK_MULTIPLIER;
            currA += deltaA;

            positions[i] = new Vector3(x, 0, z);
        }
    }

    void Update()
    {
        MoveViewfinderLock();
        PressButton();
    }

    private void MoveViewfinderFree()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        thumbStickPosition = new Vector3(x, 0.0f, z).normalized * GameManager.THUMBSTICK_MULTIPLIER;

        thumbStickMagnitude = thumbStickPosition.magnitude;
        viewfinderTrigger.IsThumbStickMoving = thumbStickMagnitude > GameManager.THUMBSTICK_THRESHOLD;

        viewfinder.localPosition = thumbStickPosition;
    }

    private void MoveViewfinderLock()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        thumbStickPosition = new Vector3(x, 0.0f, z);

        thumbStickMagnitude = thumbStickPosition.magnitude;
        viewfinderTrigger.IsThumbStickMoving = thumbStickMagnitude > GameManager.THUMBSTICK_THRESHOLD;

        if (viewfinderTrigger.IsThumbStickMoving)
        {
            float angle = Vector3.Angle(thumbStickPosition, new Vector3(0.0f, 0.0f, -1.0f));
            float cross = Vector3.Cross(thumbStickPosition, new Vector3(0.0f, 0.0f, -1.0f)).y;

            if (cross > 0.0f)
            {
                if (angle >= 0.0f && angle < 22.5f)
                    nextPosition = positions[6];
                else if (angle >= 22.5f && angle < 67.5f)
                    nextPosition = positions[7];
                else if (angle >= 67.5f && angle < 112.5f)
                    nextPosition = positions[0];
                else if (angle >= 112.5f && angle < 157.5f)
                    nextPosition = positions[1];
                else if (angle >= 157.5f && angle <= 180.0f)
                    nextPosition = positions[2];
            }
            else
            {
                if (angle >= 0.0f && angle < 22.5f)
                    nextPosition = positions[6];
                else if (angle >= 22.5f && angle < 67.5f)
                    nextPosition = positions[5];
                else if (angle >= 67.5f && angle < 112.5f)
                    nextPosition = positions[4];
                else if (angle >= 112.5f && angle < 157.5f)
                    nextPosition = positions[3];
                else if (angle >= 157.5f && angle <= 180.0f)
                    nextPosition = positions[2];
            }
        }
        else
            nextPosition = Vector3.zero;

        viewfinder.localPosition = Vector3.Lerp(viewfinder.localPosition, nextPosition, viewfinderSpeed * Time.deltaTime);
    }

    private void PressButton()
    {
        if (Input.GetButtonDown("A") || Input.GetButtonDown("B") ||
            Input.GetButtonDown("X") || Input.GetButtonDown("Y"))
            ring.GetComponent<MeshRenderer>().material = RingPressedMaterial;

        if (Input.GetButtonUp("A") || Input.GetButtonUp("B") ||
            Input.GetButtonUp("X") || Input.GetButtonUp("Y"))
            ring.GetComponent<MeshRenderer>().material = RingIdleMaterial;
    }
}
