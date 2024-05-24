using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{ 
    private float shakeThreshold = 1f; // Threshold for detecting shake movement
    private float minShakeInterval = 0.5f; // Minimum time between two shakes
    private float shakeInterval = 5; // Current interval between shakes
    private float sqrShakeThreshold; // Square of the shake threshold for optimized comparison
    private bool isShaking = false; // Bool to indicate if the device is shaking
    Vector3 lastAcceleration; //// Last recorded acceleration for shake detection

    [Header("Variables for moving")]
    private new Camera camera; //Reference to the camera. using new keyword to not override existing camera
    private Rigidbody2D rigidBody; // Reference to the player's Rigidbody2D component
    private const float movementSpeed = 8f; // Movement speed of the player
    private float inputAxis; // Input axis for horizontal movement (-1 for left, 1 for right)
    private Vector2 velocity;  // Velocity vector for smooth movement
    private Collider2D circleCollider; // Reference to the player's Collider2D component
    private bool isTouchingPole; // Flag to indicate if the player is touching a flagpole


    [Header("Variables for jumping")]
    private float maxJumpHeight = 5f; // Maximum jump height of the player
    private float maxJumpTime = 1f; // Maximum time the character spends in the air during a jump

    public bool isFalling => velocity.y < 0f && !isGrounded; // Property to check if the player is falling
    public bool isGrounded { get; private set; } // Property to check if the player is grounded
    public bool isJumping {get; private set; } // Property to check if the player is jumping
    public bool isTurning => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); // Property to check if the player is turning/. If the input axis has the oposite value of velocity in terms of positive negative
    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f || isTouchingPole == true; // Absolute number of his velocity or input axis (always positive). 0.25f threshold ensures he's moving a little before sprite changes.
    /*
    Max jumptime is devided by 2, as the character will spend half of the time ascending and descending
    a read-only property because it lacks a setter method.
    not a variable in the traditional sense because it doesn't store a value directly in memory; rather, its value is calculated based on other variables.
    */
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); //Property to calculate the force needed to achieve the desired max jump height and time
    
    public float gravity => (-2 * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2); //property to calculates the gravitational force applied to the character to make it fall back to the ground. Squaring the time to account for gravitational acceleration.
    private bool isJumpReleased = false;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Get the player's Rigidbody2D component
        circleCollider = GetComponent<Collider2D>(); // Get the player's Collider2D component
        camera = Camera.main; // Get the main camera in the scene
        sqrShakeThreshold = Mathf.Pow(shakeThreshold, 2); // Calculate the square of the shake threshold
    }

    private void OnDisable()
    {
        // Disable physics and reset variables when the script is disabled
        rigidBody.isKinematic = true;
        circleCollider.enabled = false;
        velocity = Vector2.zero;
        isJumping = false;
    }

    private void OnEnable()
    {
         // Enable physics and reset variables when the script is enabled
        rigidBody.isKinematic = false;
        circleCollider.enabled = true;
        velocity = Vector2.zero;
        isJumping = false;
    }

    void Update() //placed in update to make sure it calls this method before the HorizontalMovement
    {
        HorizontalMovement(); // Call the method responsible for horizontal movement.
        
        isGrounded = rigidBody.Raycast(Vector2.down); // Check if Mario is grounded using a downward raycast from the Rigidbody and Update the isGrounded

        if(isGrounded) //If Mario is grounded, perform grounded movement and reset the isJumpRealeased bool
        {
            GroundedMovement();
            isJumpReleased = false;
        }

        ApplyGravity(); // Apply gravity to Mario's vertical velocity.
    }

    public void HorizontalMovement() //Method for horizontal movement, left and right
    {
        float accelerationFactor = 1.5f; // Define the acceleration factor for smoothing movement
        // Update the horizontal velocity using Mathf.MoveTowards to gradually change the velocity towards the target value
        //The target value is determined by multiplying the input axis (left or right direction) by the movement speed
        // The movementSpeed * Time.deltaTime * accelerationFactor ensures smooth acceleration and deceleration
        //Mathf.Movetowards = moves the current velocity value towards the target value (inputAxis * movementspeed) slowly to simulate acceleration abd deacceleration
        //Movementspeed * Time.deltaTime = maximum change applied to the current value, ensures framerate independency and the desired acc and deacc
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime * accelerationFactor); //Calculates the target velocity for the rigid body

        if (rigidBody.Raycast(Vector2.right * velocity.x)) //checks if mario collides with wall, if true, velocity and inputAxis is set to 0
        {
            velocity.x = 0f;
            inputAxis = 0f;
        }

        //renders sprite to the right or left based on velocity, not on input.
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        //prevents gravity from building up
        velocity.y = Mathf.Max(velocity.y, 0f); // Prevents gravity from building up when the character is grounded by ensuring the vertical velocity doesn't go below 0.
        isJumping = velocity.y > 0f; // Update the 'isJumping' flag based on the vertical velocity. If the velocity is greater than 0, it means the character is moving upwards, indicating a jump.
        isShaking = false; //resets isShaking when grounded
    }

    private void ApplyGravity()
    {

        if (isShaking) // If the character is shaking (e.g., due to a shake gesture), apply increased gravity.
        {
            float multiplierShake = 50f; // Define a multiplier to increase the gravity effect during shaking.
            velocity.y += gravity * multiplierShake * 2f * Time.deltaTime; // Apply increased gravity to the vertical velocity, multiplied by the shake multiplier and time.
            velocity.y = Mathf.Max(velocity.y, gravity * multiplierShake); // Ensure the vertical velocity doesn't exceed the maximum shake-induced gravity.
            isShaking = false;
            return;

        }
        // check if falling
        bool isFalling = velocity.y < 0f || isJumpReleased == true; //checks if character is falling, returns true if velocity is negatve (falling downw)
        float multiplier = isFalling ?2f : 1f; // Apply multiplier based on falling state, falls faster when hes falling by multiplying with 2

        velocity.y += gravity * multiplier * Time.deltaTime; // Increment the vertical velocity by applying gravity, multiplied by the gravity multiplier and time.
        velocity.y = Mathf.Max(velocity.y, gravity / 2f); // Ensure the vertical velocity doesn't fall below half of the maximum gravity value, setting a terminal velocity to prevent the character from falling too fast
    }

    void FixedUpdate() //physics for physics
    {
        Vector2 position = rigidBody.position; //gets the current position of the rigidbody
        position += velocity * Time.fixedDeltaTime; // Update the position based on the calculated velocity, multiplied by time.fixedDeltaTime to ensure frame rate independence

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); //Converts the screen coordinate (0, 0) to a world coordinate using ScreenToWorldPoint. Vector2.zero represents the screen coordinate (0, 0), which corresponds to the bottom-left corner of the screen
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); //width and height represents the width of the screen in pixels. new Vector2(Screen.width, Screen.height) represents the screen coordinate at the top-right corner of the screen.
    
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); //restrict the x-coordinate of players position within a specific range. 0.5+- ensures it stays slightly to the left and right of the edges.

        rigidBody.MovePosition(position); //move the rigidbody to the updated position
        DetectShake(); // Check for shake movement
    }

    private void DetectShake()
    {
        Vector3 acceleration = Input.acceleration;  // Retrieve the current device's acceleration data.
        Vector3 deltaAcceleration = acceleration - lastAcceleration; // Calculate the change in acceleration since the last frame.

        if (deltaAcceleration.sqrMagnitude >= sqrShakeThreshold && shakeInterval >= minShakeInterval && isJumping) // if change in acc exceed threshold, if shakeInterval exceed threshold and if isJump is true
        {
            // Set the bool indicating a shake gesture occurred and reset the shake interval.
            isShaking = true;
            shakeInterval = 0f;
        }
        shakeInterval += Time.deltaTime; // Update the shake interval by adding the time since the last frame.
        lastAcceleration = acceleration; // Update the last acceleration for use in the next frame's calculations.
    }

    // Methods for horizontal movement
    public void MoveLeft() //Method to initiate left movement
    {
        inputAxis = -1f;
    }

    public void MoveRigth() //Method to initiate right movement
    {
        inputAxis = 1f;
    }

    public void StopMovement() //Method to stop movement
    {
        inputAxis = 0f;
    }

    //Methods for jumping
    public void Jump() //Called on trigger event PointerDown
    {
        if (isGrounded)
        {
            velocity.y = jumpForce; //add the jump force to make the character jump
            isJumping = true; //sets isJumping to true
            isJumpReleased = false; //sets isJumpReleased to false
        }
    }

    public void ReleaseJump()
    {
        isJumpReleased = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) //checks if character collides with a block above him to or jumps on top of an enemy
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //// If the collision is with an enemy by checking the layer
        {
            if (transform.DotTest(collision.transform, Vector2.down)) // If the character is above the enemy.
            {
                velocity.y = jumpForce;  // Apply vertical velocity to simulate jumping.
                isJumping = true; // Set the 'isJumping' flag to true.
                
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))  // If the collision is not with a power-up.
        {
            if(transform.DotTest(collision.transform, Vector2.up)) // If the character is below the collided object.
            {
                velocity.y = 0f;// Stop vertical movement.
            }
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flagpole")) // If the trigger collider belongs to the flagpole.
        {
            isTouchingPole = true; // Set the 'isTouchingPole' flag to true.
        }
    }





    
}
