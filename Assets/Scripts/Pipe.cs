using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection; // The transform of the pipe's connection.
    private float shakeThreshold = 3.0f; // The threshold for triggering a shake effect
    private float sqrShakeThreshold;  // The square of the shake threshold for optimization.
    public Vector3 enterDirection = Vector3.down; // The direction from which the player enters the pipe (default is down). Can be changed in inspector
    public Vector3 exitDirection = Vector3.zero; // The direction in which the player exits the pipe (default is zero, indicating no specific exit direction).

    private void Start()
    {
        sqrShakeThreshold = Mathf.Pow(shakeThreshold, 2); // Calculates the square of the shake threshold for optimization purposes.
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && connection != null && Input.acceleration.sqrMagnitude >= sqrShakeThreshold) // Checks if the colliding object is tagged as "Player", if there's a valid connection and if shakeThreshold is met
        {
            StartCoroutine(EnterPipe(other.transform));// Initiates the EnterPipe coroutine if the conditions are met.
        }
        
    }

    private IEnumerator EnterPipe(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false; // Disables the player's movement.
        Vector3 enteredPosition = transform.position + enterDirection; // Calculates where the player needs to enter the pipe.  if enterDirection is Vector3.down, it moves the entry point downwards from the pipe's position, into the pipe
        Vector3 enteredScale = Vector3.one * 0.5f; // Set the scale for the player upon entering the pipe (makes Mario small).

        yield return Move(player, enteredPosition, enteredScale); // Moves the player to the entered position and scales the player. (enter the pipe)
        yield return new WaitForSeconds(1f); // Delays for 1 second

        bool isUnderground = connection.position.y < 0; // Checks if the connection is underground, if the connections y position is below 0
        Camera.main.GetComponent<SideScrolling>().SetCameraUnderground(isUnderground); // Sets the camera to underground mode if isUndergorund is true

        if (exitDirection != Vector3.zero) // Check if there's an exit direction specified.
        {
            player.position = connection.position - exitDirection; // Moves the players position to the connection positon minus the exitDirection ,making sure hes not seen at the beginning
            yield return Move(player, connection.position + exitDirection, Vector3.one); // Moves the player from their current position to the connection position + exitDirection and scales him to 1,1,1 (exits pipe)
        }
        else // if the exitDirection is 0.0.0
        {
            player.position = connection.position; // Sets the player's position to the connection point. (Spawns mid air undergorund)
            player.localScale = Vector3.one; // Resets the player's scale.
        }
        player.GetComponent<PlayerMovement>().enabled = true; // Enables the player's movement.
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f; // Time elapsed since the start of the movement.
        float duration = 1f; // Duration of the movement.

        Vector3 startPosition = player.position; // Starting position of the player.
        Vector3 startScale = player.localScale; // Starting scale of the player.

        while (elapsed < duration) //while loop, that loops while the elapsed time is less than the duration of the movement
        {
            float t = elapsed / duration; // Calculate the interpolation factor (t) based on elapsed time and duration.
            player.position = Vector3.Lerp(startPosition, endPosition, t); // Interpolate the players position between the start and end positions. (Linear interpolation)
            player.localScale = Vector3.Lerp(startScale, endScale, t); // Interpolate the players scale between the start and end scales. (Linear interpolation)

            elapsed += Time.deltaTime; // Increment the elapsed time by the time since the last frame.

            yield return null; // Wait for the next frame before continuing the loop, creating a smooth animation over multiple frames.
        }

        player.position = endPosition;// Ensure the players position is exactly the target position at the end of the movement.
        player.localScale = endScale; // Ensure the players scale is exactly the scale position at the end of the movement.

    }

}
