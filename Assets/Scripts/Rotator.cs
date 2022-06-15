using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        //Rotate our object around an axis over time
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime * speed, Space.World);
    }
}
