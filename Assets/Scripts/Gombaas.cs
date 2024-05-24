using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gombaas : Enemy //inherets from Enemy class
{
    [SerializeField] private Sprite flatSprite; // Sprite to be used when the Goomba is flattened

    protected override void Hit()  // Override the Hit method from the base Enemy class
    {
        if (enemyLives > 1) // Check if the Goomba has more than one life
        {
            SubtractLife(); // Subtract one life from the Goomba
        }
        else if (enemyLives == 1) // Check if the Goomba has exactly one life left
        {
            Flatten(); // Call the Flatten method to flatten the Goomba
        }
    }

    private void Flatten() // Private method to handle the Goomba flattening
    {
        GetComponent<Collider2D>().enabled = false; // Disable the Goomba's collider to stop interactions
        entityMovement.enabled = false; // Disable the movement script so the Goomba stops moving
        animatedSprites.enabled = false; // Disable the animation script to stop animations
        spriteRenderer.sprite = flatSprite; // Change the sprite to the flattened sprite
        Destroy(gameObject, 0.5f); // Destroy the Goomba game object after a delay of 0.5 seconds
    }
}


