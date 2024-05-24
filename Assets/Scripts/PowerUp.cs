using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type // Enumeration for different types of power-ups.
    {
        Coin,
        SuperCoin,
        ExtraLife,
        MagicMushroom,
        StarPower,
    }

    public Type type; // The type of power-up set in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the entering collider has the tag "Player".
        {
            Collect(other.gameObject);  // Call the Collect method passing the player's game object.
        }
    }

    private void Collect(GameObject player)  // Method to handle collecting the power-up.
    {
        switch (type) // Switch statement based on the type of power-up.
        {
            case Type.Coin:
            GameManager.Instance.AddCoin(); // Add a coin.
                break;
            
            case Type.SuperCoin:
            GameManager.Instance.AddCoin(50);// Add 50 coins with a higher value.
                break;
            
            case Type.ExtraLife:
            GameManager.Instance.AddLife(); // Add an extra life.
                break;

            case Type.MagicMushroom:  // Mushroom power-up for player growth.
                player.GetComponent<Player>().Grow(); // Trigger the player's growth.
                break;

            case Type.StarPower: // Star power-up for temporary invincibility.
                player.GetComponent<Player>().StarPower(); // Trigger the player's star power.
                break;
        }

        Destroy(gameObject); // Destroy the power-up object after collection.
    }
}
