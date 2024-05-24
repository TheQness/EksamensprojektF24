using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } // Reference to the SpriteRenderer component.
    private PlayerMovement playerMovement;

    public Sprite idle; // Sprite for idle state.
    public Sprite jump; // Sprite for jumping state.
    public Sprite turn;  // Sprite for turning state.
    public AnimatedSprites run; // Animated sprites for running state.
        
    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>(); // Get the PlayerMovement component from the parent object.
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component from this object.
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true; // Enable the sprite renderer when the object is enabled.
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false; // Disable the sprite renderer when the object is disabled.
        run.enabled = false; // Disable the animated sprites for running.
    }

    private void LateUpdate() //physics is updated before changing the sprites
    {
        run.enabled = playerMovement.isRunning; // Enable or disable the running animation based on the player's movement.
        if (playerMovement.isJumping) // If the player is jumping.
        {
            spriteRenderer.sprite = jump; // Set the sprite to the jumping sprite.
        }
        else if (playerMovement.isTurning)
        {
            spriteRenderer.sprite = turn;
        }

        else if (!playerMovement.isRunning)
        {
            spriteRenderer.sprite = idle;
        }

    }
}
