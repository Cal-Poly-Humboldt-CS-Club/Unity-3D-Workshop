// These will get added automatically when creating a script with Unity.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You need to add this to use the new InputSystem.
using UnityEngine.InputSystem;

// You need this to use TextMeshPro in the script.
using TMPro;

// This class name is how Unity references your code. Make sure that it matches
// the name of the script.
public class PlayerController : MonoBehaviour
{
    public float speed = 0; // A float variable to assign speed. Since we
                            // declared it as public, that means that we can
                            // change it back in the Unity editor.
    private Rigidbody rb; // A Rigidbody object so that we can reference the
                          // player's Rigidbody component. This is how we will
                          // affect the physics of the player.
    private float movementX, movementY; // These floats are for recieving the
                                        // movement input.
    private int count; // This int variable will keep track of the amount of
                       // PickUps that we have picked up.
    public TextMeshProUGUI countText; // This lets us reference the UI count
                                      // text in the game.
    public GameObject winTextObject; // Here we have a gameobject for the
                                     // game's win message.

    // Here we define our own function called SetCountText that will update the
    // countText with the correct value.
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        // This will display the win message once 8 pickups have been collected
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    // Start is called once before the first frame of the game. This makes it
    // good for things that you only want to happen once, before the game
    // actually starts running.
    void Start()
    {
        // Here we assign rb to the Rigidbody component of the Player. We don't 
        // actually have to do this, but it lets us type 'rb' instead of 
        // 'GetComponent<Rigidbody>()' every time we want to affect the physics
        // of the player.
        rb = GetComponent<Rigidbody>();
        // This sets the count to 0 at the start of the game.
        count = 0;
        // This will call the SetCountText function so that we have the Count:
        // displaying from the start of the game.
        SetCountText();
        // This disables the win message at the start of the game.
        winTextObject.SetActive(false);
    }

    // OnMove is a predefined function in the new InputSystem. It will be
    // called whenever wasd, the arrow keys, or a joystick recieve input. It
    // recieves the input value as a parameter that we can use to fill our
    // movement vector.
    void OnMove(InputValue movementValue) 
    {
        // Here we set movementX and Y equal to the movement value's Vector2
        // Note: I believe that the movementValue Vector2 just reads as 1, 0,
        // or -1 based on if it is detecting forward input, no input, or back
        // input. That's how it works when you define your own input actions,
        // but OnMove is prebuilt so it might be doing something different, 
        // maybe worth looking into.
        movementX = movementValue.Get<Vector2>().x;
        movementY = movementValue.Get<Vector2>().y;
    }

    // FixedUpdate gets called in sync with the physics engine. This means that
    // the code within fixed update will execute everytime that the physics of
    // the scene are updated. It's usually better to use FixedUpdate instead of
    // Update when using forces or any other physics-based functions, since 
    // Update can run out of sync with the physics engine. 
    void FixedUpdate()
    {
        // Here we create a Vector3 to store where we want to move on the X
        // and Z axes. Note that the Vector2 that we used earlier only has X
        // and Y axes, but we can map Y to Z because pressing 'up' or 'down'
        // really means that we want to move forward or back.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // We use a function of Rigidbody called AddForce to actually move the
        // player and we multiply our movement Vector by the speed variable 
        // that we declared earlier.
        rb.AddForce(movement * speed);
    }
    // OnTriggerEnter is called every time the gameobject touches a trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Here we have a conditional that will fire off if the tag of the
        // collider we just touched matches "PickUp".
        if (other.gameObject.CompareTag("PickUp"))
        {
            // If the tag of other is "PickUp" then we set other to false.
            other.gameObject.SetActive(false);
            // We increment count every time we pick up a PickUp
            count++;
            // We call SetCountText to update the count text every time that
            // we collect another pickup.
            SetCountText();
        }
    }
}
