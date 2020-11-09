using UnityEngine;

[ExecuteInEditMode]
public class SetParticleRing : MonoBehaviour
{
    public GameObject ParticleRingPrefab;
    public Material EmissiveGreen;
    public Material EmissiveRed;
    public Material EmissiveBlue;
    public Material EmissiveYellow;
    public bool SetRing;
    public Material EmissiveButtonsMaterial;
    public bool SetMaterial;
    public bool SetRingPosition;
    public bool DeleteParticles;

    void Update()
    {
        if (DeleteParticles)
        {
            DeleteParticles = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<ParticleSystem>() != null)
                    DestroyImmediate(transform.GetChild(i).GetComponent<ParticleSystem>());
            }
        }

        if (SetRing)
        {
            SetRing = false;

            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).childCount < 3)
                    {
                        GameObject particleRing = Instantiate(ParticleRingPrefab, transform.GetChild(i));

                        switch (transform.GetChild(i).GetComponent<NoteBehaviour>().InputKey.ToString())
                        {
                            case "A":
                                transform.GetChild(i).GetChild(2).GetComponent<MeshRenderer>().material = EmissiveGreen;
                                break;
                            case "B":
                                transform.GetChild(i).GetChild(2).GetComponent<MeshRenderer>().material = EmissiveRed;
                                break;
                            case "X":
                                transform.GetChild(i).GetChild(2).GetComponent<MeshRenderer>().material = EmissiveBlue;
                                break;
                            case "Y":
                                transform.GetChild(i).GetChild(2).GetComponent<MeshRenderer>().material = EmissiveYellow;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        if (SetMaterial)
        {
            SetMaterial = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().material = EmissiveButtonsMaterial;
            }
        }

        if (SetRingPosition)
        {
            SetRingPosition = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetChild(2) != null)
                    transform.GetChild(i).GetChild(2).transform.localPosition = new Vector3(0.0f, -0.1f, 0.0f);
            }
        }
    }
}
