using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 direction = Vector2.left;
    public float speed = 1f;
    private Vector2 velocity;
    private SpriteRenderer spriteRenderer;

    private float smallRaycastDistance = 0.375f;
    private float mediumRaycastDistance = 0.6f;
    public float largeRaycastDistance = 1.1f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = true;
    }

    private void OnEnable()
    {
        rigidBody.WakeUp();
    }

    private void OnDisable()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        
        float raycastDistance = GetRaycastDistance();
        if (rigidBody.Raycast(direction, raycastDistance))
        {
            direction = -direction;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (rigidBody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    private float GetRaycastDistance()
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
            // Default to small multiplier if scale doesn't match predefined sizes
            return smallRaycastDistance;
        }
    }
}

