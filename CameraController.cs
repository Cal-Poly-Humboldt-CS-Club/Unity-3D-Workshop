using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will make the camera track the player as it moves around.
public class CameraController : MonoBehaviour
{
    public GameObject player; // gameobject to reference the player.
    private Vector3 offset; // Vector3 to keep track of the offset from the
                            // player to the camera.

    // Start is called before the first frame update
    void Start()
    {
        // Here we perform the calculation to find the exact distance from the
        // player to the camera in 3D space.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame, but after all the other update
    // functions are done. This makes it perfect for things that you want to
    // happen AFTER the physics and other processes have already happened.
    void LateUpdate()
    {   
        // after all other functions have fired off in the frame, we move the
        // camera to be centered on the player.
        transform.position = player.transform.position + offset;
    }
}
