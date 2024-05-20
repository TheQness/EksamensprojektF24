using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{ 
    [Header("Variables for moving")]
    private new Camera camera; //using new keyword to not override existing camera
    private Rigidbody2D rigidBody; // Reference to the Rigidbody2D component of the player
    private const float movementSpeed = 8f; // Movement speed of the player
    private float inputAxis; // Input axis for horizontal movement (-1 for left, 1 for right)
    private Vector2 velocity;  // Velocity vector for smooth movement


    [Header("Variables for jumping")]
    private float maxJumpHeight = 5f; //max jump height of 5 units/blocks
    private float maxJumpTime = 1f; // max jumptime the character spends in the air, 1 second

    public bool isGrounded { get; private set; }
    public bool isJumping {get; private set; } // bool to check if the character is jumping. Can be accessed from outside the class but can only be modified within the class.
    public bool isTurning => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); //turning if the input axis has the oposite value pof velocity in terms of positive negative
    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f; // gets the absolute number of his velocity or input axis (always positive), and if it above 0.25, mario is running. 0.25 ensures his moving a little before sprite changes.
    //public bool isIdle => Mathf.Abs(velocity.x) == 0f && Mathf.Abs(inputAxis) > 0.25f;
    /*
    property (not variable) that calculates the force needed to achieve the desired max jump height and time using the jumpForce formula. 
    Max jumptime is devided by 2, as the character will spend half of the time ascending and descending
    a read-only property because it lacks a setter method.
    not a variable in the traditional sense because it doesn't store a value directly in memory; rather, its value is calculated based on other variables.
    */
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); 
    /*
    property that calculates the gravitational force applied to the character to make it fall back to the ground. Squaring the time to account for gravitational acceleration.
    */
    public float gravity => (-2 * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2); 
    //public bool isGrounded {get; private set; } // bool to check if the character is grounded. Can be accessed from outside the class but can only be modified within the class. (encapsulation).
   
    private bool isJumpReleased = false;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Getting reference to the Ridigbody component of Mario
        camera = Camera.main; //getting reference to the main camera in the scene
    }

    void Update() //placed in update to make sure it calls this method before the HorizontalMovement
    {
        HorizontalMovement();
        
        isGrounded = rigidBody.Raycast(Vector2.down); //checks if mario is grounded through raycast extension

        if(isGrounded) 
        {
            GroundedMovement();
            isJumpReleased = false;
        }

        ApplyGravity(); //applies gravity
    }

    void FixedUpdate() //physics for physics
    {
        Vector2 position = rigidBody.position; //gets the current position of the rigidbody 

        //updates the position based on the calculated velocity, multiplied by time.deltatime to ensure frameraye independency
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); //Converts the screen coordinate (0, 0) to a world coordinate using ScreenToWorldPoint. Vector2.zero represents the screen coordinate (0, 0), which corresponds to the bottom-left corner of the screen
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); //width and height represents the width of the screen in pixels. new Vector2(Screen.width, Screen.height) represents the screen coordinate at the top-right corner of the screen.
        //Mathf.Clamp to restrict the x-coordinate of a position within a specific range. position.x represents the x-coordinate of the position that needs to be clamped. 
        // 0.5+- ensures it stays slightly to the left and right of the edges. Clamp ensures that the position.x remains within the range of left an right edge
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidBody.MovePosition(position); //move the rigidbody to the updated position
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

    public void HorizontalMovement() //Method for horizontal movement, left and right
    {
         //Calculates the target velocity for the rigid body
        //velocity.x = current velocity of the rigid body in the x-axis
        //Mathf.Movetowards = moves the current velocity value towards the target value (inputAxis * movementspeed) slowly to simulate acceleration abd deacceleration
        //inputAxis * movementSpeed = the target value for the velocity
        //Movementspeed * Time.deltaTime = maximum change applied to the current value, ensures framerate independency and the desired acc and deacc
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);

        if (rigidBody.Raycast(Vector2.right * velocity.x)) //checks if mario collides with wall, if true, velocity is set to 0
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

    //Methods for jumping
    public void Jump() //Called on trigger event PointerUp
    {
        if (isGrounded)
        {
            velocity.y = jumpForce; //add the jump force to make the character jump
            isJumping = true; //sets isJumping to true
            return;
        }
    }

    private void ApplyGravity()
    {
        // check if falling
        bool isFalling = velocity.y < 0f || isJumpReleased == true; //checks if character is falling, returns true if velocity is negatve (falling downw)
        float multiplier = isFalling ?2f : 1f; // Apply multiplier based on falling state, falls faster when he´s falling by multiplying


        // apply gravity and terminal velocity
        velocity.y += gravity *multiplier * Time.deltaTime; //
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    public void ReleaseJump()
    {
        isJumpReleased = true;
    }
        
    private void GroundedMovement()
    {
        //prevents gravity from building up
        velocity.y = Mathf.Max(velocity.y, 0f); //ensures velocity does not go below 0 or, returning the largest of the two values.
        isJumping = velocity.y > 0f; //checks isJumping based on vertical velocity, is true if velocity is positive (going up)
    }

    //METHODS FOR COLLISIONS
    //checks if character collides with a block above him to or jumps on top of an enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce/2f; //half jumpsize
                isJumping = true;
                
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }





    
}
