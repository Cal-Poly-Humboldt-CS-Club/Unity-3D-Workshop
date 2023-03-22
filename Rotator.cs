using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script makes anything that it is attached to rotate.
public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // We use the Rotate function to rotate the attached gameobject in real
        // time.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
