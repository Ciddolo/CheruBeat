using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float AnglePerSecond;

    void Update()
    {
        transform.Rotate(Vector3.up, AnglePerSecond * Time.deltaTime);
    }
}
