using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for instantiating block coins from blocks, animating their movement, 
    /// and adding the coins to the player's total when this script is enabled through the BlockHit script.
    /// </summary>

    private void Start()
    {
        GameManager.Instance.AddCoin(); // Adds a coin to the playrs total coins through the AddCoin() method from the GameManager
        StartCoroutine(Animate()); // Starts the animation coroutine for the coin.
    }

    private IEnumerator Animate() //Coroutine to handle the coin's animation. It moves the coin up and then back down and finally destroys the coin object.
    {
        Vector3 restingPosition = transform.localPosition; // The initial position of the coin.
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f; // The target position of the coin after it moves up.
        yield return Move(restingPosition, animatedPosition); // Move the coin from the resting position to the animated position through the Move method.
        yield return Move(animatedPosition, restingPosition); // Move the coin back from the animated position to the resting position, so it hides behind the block.

        Destroy(gameObject); //Destroys the coin
    }
    
    private IEnumerator Move(Vector3 from, Vector3 to) // Coroutine to move the coin from one position to another over a set duration.
    {
        float elapsed = 0f;// The amount of time that has passed since the movement started.
        float duration = 0.25f; //The duration of the animation

        while (elapsed < duration) //while loop, that loops while the elapsed time is less than the duration of the animation
        {
            float t = elapsed / duration; // Calculate the interpolation factor (t) based on elapsed time and duration.
            transform.localPosition = Vector3.Lerp(from, to, t); // Interpolate the coin's position between the start and end positions. (Linear interpolation)
            elapsed += Time.deltaTime; // Increment the elapsed time by the time since the last frame.

            yield return null; // Wait for the next frame before continuing the loop, creating a smooth animation over multiple frames.
        }

        transform.localPosition = to; // Ensure the coin's position is exactly the target position at the end of the movement.

    }
}
