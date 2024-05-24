using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
     /// <summary>
    /// This script handles the animation and activation of items that instantiates from blocks. This scripts is enabled when the items is instantiated
    /// </summary>

    private void Start() //Fires when item is instantiated
    {
        StartCoroutine(Animate()); // Start the animation coroutine when the script starts.
    }

    private IEnumerator Animate()
    {
        // Retrieve necessary components from the game object.
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>(); //this is the collider that ensures it colliders with the ground and pipes and so
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>(); //trigger collider larger than the box collider, that triggers collecting, when mario collidrs with this
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Disable physics and rendering initially.
        rigidBody.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f); // Wait for 0.25 seconds before starting the animation. Ensures BlockHit animation is done

        spriteRenderer.enabled = true; // Enable the sprite renderer to make the item visible.

        float elapsed = 0f; // The amount of time that has passed since the animation started.
        float duration = 0.5f; // The duration of the animation.

        // Set the start and end positions for the animation.
        Vector3 startPosition = transform.localPosition; // The initial position of the item to its current spawning position
        Vector3 endPosition = transform.localPosition + Vector3.up; // The final position of the item after moving up.

        while (elapsed < duration) //while loop, that loops while the elapsed time is less than the duration of the animation
        {
            float t = elapsed / duration; // Calculate the interpolation factor (t) based on elapsed time and duration.
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);  // Interpolate the blocks position between the start and end positions. (Linear interpolation)
            elapsed += Time.deltaTime; // Increment the elapsed time by the time since the last frame.

            yield return null; // Wait for the next frame before continuing the loop, creating a smooth animation over multiple frames.
        }

        
        transform.localPosition = endPosition; // Ensure the coin's position is exactly the target position at the end of the movement.

        // Re-enable physics and rendering after the animation.
        rigidBody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
        spriteRenderer.enabled = true;

    }
}
