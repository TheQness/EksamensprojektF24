using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    /// <summary>
    /// The scripts handles the interaction when a player hits a block in a 2D game. It animates the block's movement, changes its sprite, and optionally spawns an item.
    /// </summary>
    public int maxHits = -1; // The maximum number of times the block can be hit before it becomes inactive. A value of -1 means infinite hits. Is changed in inspector
    public Sprite emptyBlock; // The sprite to display when the block is out of hits. Set in inspector
    private bool animating; // A bool to check if the block is currently animating.
    public GameObject item; // The item to instantiate when the block is hit.
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (!animating && maxHits !=0 && collision.gameObject.CompareTag("Player")) // Check if the block is not animating, has remaining hits, and the collider is the player.
        {
            if (collision.transform.DotTest(transform, Vector2.up)) // Check if the collision is from below (player hitting the block from below).
            {
                Hit(); //Calls the hit method
            }
        }
    }

    private void Hit()
    {
        spriteRenderer.enabled = true; // Ensure the sprite renderer is enabled. Makes sure invisble blocks becuase visible when hiddn
        maxHits--; //decrement the number of reamining hits
        if (maxHits == 0) // If the block has no remaining hits, change its sprite to the empty block sprite.
        {
            spriteRenderer.sprite = emptyBlock;
        }
        if (item !=null) // If there's an item to spawn, instantiate it at the block's position.
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());  // Start the block animation coroutine.
    }

    private IEnumerator Animate() //Handles the animation of the block when hit, moving it up and then back to its original position.
    {
        animating = true; // Set the animating bool to true to prevent multiple animations at once.
        Vector3 restingPosition = transform.localPosition; // Saves the block's original position in Vector3 restingPosition
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f; // Calculates and the block's position after being hit (moved up), by adding a Vector3.up times a half to the vector restingposition
        yield return Move(restingPosition, animatedPosition);  // Moves the block from resting position to the animated position throughy the Move method
        yield return Move(animatedPosition, restingPosition); // Move the block back to the resting position.

        animating = false;// Sets animating bool to false, ensuring another animation can now be made.
    }

    private IEnumerator Move(Vector3 from, Vector3 to) //Handles the smooth movement of the block between two positions using linear interpolation.
    {
        float elapsed = 0f; // Time elapsed since the start of the movement.
        float duration = 0.125f;  // Duration of the movement.

        while (elapsed < duration) //while loop, that loops while the elapsed time is less than the duration of the animation
        {
            float t = elapsed / duration; // Calculate the interpolation factor (t) based on elapsed time and duration.
            transform.localPosition = Vector3.Lerp(from, to, t); // Interpolate the blocks position between the start and end positions. (Linear interpolation)
            elapsed += Time.deltaTime; // Increment the elapsed time by the time since the last frame.

            yield return null; // Wait for the next frame before continuing the loop, creating a smooth animation over multiple frames.
        }

        transform.localPosition = to; // Ensure the coin's position is exactly the target position at the end of the movement.

    }
}
