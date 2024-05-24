using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))] // Require the component SpriteRenderer to be attached to the same GameObject
public class AnimatedSprites : MonoBehaviour
{
    public Sprite[] sprites; //Array of sprites used for animation, lemtgh and sprites are set in inspector
    public float frameRate = 1f / 6f; // The rate at which the animation frames change. 1/6 seconds per frame (6 frames per second) 

    private SpriteRenderer spriteRenderer; //Variable to hold reference to Spriterenderer script on the GameObjects
    private int frame; // Current frame index for animation, used to keep track of the current frame index in the sprites array

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to this GameObject
    }

    private void OnEnable() //Called when the object becomes enabled and active. This scripts is enabled and disabled in the x script
    {
        InvokeRepeating(nameof(Animate), frameRate, frameRate); // Start the Animate method repeatedly at the specified frameRate
    }

    private void OnDisable() // Called when the object becomes disabled.
    {
        CancelInvoke(); // Stop the InvokeRepeating when the object is disabled
    }

    private void Animate()
    {
        frame++; //Incrementing the frame index.

        if(frame >= sprites.Length) //Resetting the frame index to 0 if it exceeds the length of the sprites array.
        {
            frame = 0;
        }
        if(frame >= 0 && frame < sprites.Length) // Ensure the frame index is valid before assigning the sprite
        {
            spriteRenderer.sprite = sprites[frame]; // Set the sprite to the current frame
        }

    }

}
