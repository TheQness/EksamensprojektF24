using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for detecting Marios collison with Flagpole and performing the Level Complete Sequence and loading the next level.
    /// </summary>

    public Transform flag; // The flag object at the top of the pole.
    public Transform poleBottom; //// The bottom of the flag pole.
    public Transform castle; // The castle to move the player towards.
    private float speed = 4f; // The speed at which objects move.
    private float distanceThreshold = 0.125f; // The minimum distance required to consider an object having "reached" its destination.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //checks if the omject collding with flagpole is Mario
        {
            StartCoroutine(MoveTo(flag, poleBottom.position)); // Moves the flag to the bottom of the pole.
            StartCoroutine(LevelCompleteSequence(other.transform)); //Moves mario to the bottom of the pole and to the castle
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player) // Sequence of movements to complete the level.
    {
        player.GetComponent<PlayerMovement>().enabled = false;  // Disable player movement during the level completion sequence.
        yield return MoveTo(player, poleBottom.position); // Move the player to the bottom of the flag pole.
        yield return MoveTo(player, player.position + Vector3.right); // Move the player to the right after reaching the bottom of the pole.
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down); // Move the player diagonally down to the right..
        yield return MoveTo(player, castle.position); // Move the player towards the castle.
        player.gameObject.SetActive(false); // Deactivate the player object when he reached the castle

        yield return new WaitForSeconds(2f);  // Wait 2 seconds before initiating the next level.

        GameManager.Instance.NextLevel();  // Load the next level.
    }
    
    private IEnumerator MoveTo(Transform subject, Vector3 destination) // Coroutine to smoothly move an object to a destination.
    {
        while (Vector3.Distance(subject.position, destination) > distanceThreshold) // Continue moving while the distance is smaller than the threshold
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime); // Move the subject towards the destination at a constant speed.
            yield return null;
        }
        subject.position = destination; // Ensure the subject reaches the exact destination.
    }
}
