using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite deathSprite; // Sprite to be displayed during the death animation.
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component.
    private float upwardsVelocity = 10f; // Initial upward velocity for the death animation.
    private float gravity = -36f; // Acceleration due to gravity.


    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Acceses the SpriteRenderer component
    }

    private void OnEnable()
    {
        UpdateSprite(); // Updates the sprite to the death sprite.
        DisablePhysics(); // Disables physics components and movement scripts.
        StartCoroutine(Animate()); // Initiates the death animation coroutine.
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true; // Ensures the sprite renderer is enabled.
        spriteRenderer.sortingOrder = 10; //makes sure that when the character dies, the character is shown falling, by setting its sorting order to 10
        if (deathSprite !=null)
        {
            spriteRenderer.sprite = deathSprite; // Changed its sprite to the deathsprite.
        }
      
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); // Retrieves all collider components in an array
        foreach (Collider2D collider in colliders) //Disables each collider.
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true; // Makes the Rigidbody2D kinematic to disable physics interactions.
        PlayerMovement playerMovement = GetComponent<PlayerMovement>(); // Retrieves the PlayerMovement script if attached.
        EntityMovement entityMovement = GetComponent<EntityMovement>();// Retrieves the EntityMovement script if attached.

        if (entityMovement != null)
        {
            entityMovement.enabled = false; // Disables the EntityMovement script if not null.
        }
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disables the playerMovement script if not null
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f; // The amount of time that has passed since the animation started.
        float duration = 3f; // The duration of the animation.
        Vector3 velocity = Vector3.up * upwardsVelocity; // Calculation of upward velocity vector.
        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime; // Moves the object upward based on velocity.
            velocity.y += gravity * Time.deltaTime; // Applies gravity to the velocity.
            elapsed += Time.deltaTime; // Updates the elapsed time.
            yield return null; // Waits for the next frame.
        }
    }
}
