using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody; // Reference to the Rigidbody2D component.
    public Vector2 direction = Vector2.left; // Movement direction, default is left.
    public float speed = 1f; // Movement speed.
    private Vector2 velocity; // Current velocity of the entity.
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component.

    // Raycast distances for different entity sizes.
    private float smallRaycastDistance = 0.375f;
    private float mediumRaycastDistance = 0.6f;
    public float largeRaycastDistance = 1.1f;

    private void Awake()
    {
        // Get the Rigidbody2D and SpriteRenderer components.
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false; // Disable the script initially
    }

    private void OnBecameVisible()
    {
        enabled = true; //Starts movement when Entity becomes visble
    }

    private void OnBecameInvisible()
    {
        enabled = false; //Stops movement when Entity becomes visble
    }

    private void OnEnable()
    {
        rigidBody.WakeUp(); // Wake up the Rigidbody2D component.
    }

    private void OnDisable()
    {
        // Reset the velocity and put the Rigidbody2D component to sleep.
        rigidBody.velocity = Vector2.zero;
        rigidBody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed; // Calculate the horizontal velocity based on the direction and speed.
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; // Apply gravity to the vertical velocity.
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime); // Move the Rigidbody2D position based on the velocity.
        
        float raycastDistance = GetRaycastDistance(); // Calculate the raycast distance based on the entity's scale.
        if (rigidBody.Raycast(direction, raycastDistance)) // Perform raycasting to detect obstacles.
        {
            // If an obstacle is detected, change the direction and flip the sprite horizontally.
            direction = -direction;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (rigidBody.Raycast(Vector2.down)) // Checks if the entity is grounded.
        {
            velocity.y = Mathf.Max(velocity.y, 0f); // Limit the downward velocity to prevent falling through platforms.
        }
    }

    private float GetRaycastDistance() // Calculate the appropriate raycast distance based on the entity's scale.
    {
        Vector3 scale = transform.localScale;

        if (scale == new Vector3(1, 1, 1))
        {
            return smallRaycastDistance;
        }
        else if (scale == new Vector3(1.5f, 1.5f, 1.5f))
        {
            return mediumRaycastDistance;
        }
        else if (scale == new Vector3(2f, 2f, 2f))
        {
            return largeRaycastDistance;
        }
        else
        {
            // Default to mediumRaycastDistance if scale doesn't match predefined sizes
            return mediumRaycastDistance;
        }
    }
}

