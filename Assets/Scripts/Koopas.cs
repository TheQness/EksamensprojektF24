using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Koopas : Enemy
{
    [SerializeField] private Sprite shellSprite; //sprite to use when the Koopa is in its shell
    public bool isShelled; // Bool to check if the Koopa is in its shell state
    public bool isPushed; // Bool to check if the Koopa's shell is being pushed
    private float shellSpeed = 12f; // Speed of the Koopa's shell when pushed

    protected override void Awake() // Override the Awake method from the base Enemy class
    {
        // Initialize base class fields and properties
        animatedSprites = GetComponent<AnimatedSprites>(); // Get reference to the AnimatedSprites component
        deathAnimation =  GetComponent<DeathAnimation>(); // Get reference to the DeathAnimation component
        livesText = GetComponentInChildren<TMP_Text>(); // Get reference to the TMP_Text component in children
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityMovement = GetComponent<EntityMovement>();
        mediumYOffset = 0.38f; // Set medium Y offset
        largeYOffset = 0.75f; // Set large Y offset
    }

    protected override void Hit()  // Override the Hit method from the base Enemy class
    {
        if (enemyLives > 1) // Check if the Koopa has more than 1 life
        {
            SubtractLife(); // Subtract one life from the Koopa
        }
        else if (enemyLives == 1) // Check if the Koopa has exactly one life left
        {
            EnterShell(); // Call the EnterShell method to enter the shell state
        }
    }

    private void EnterShell() // Private method to handle the Koopa entering its shell state
    {
        isShelled = true; // Set isShelled to true
        entityMovement.enabled = false; // Disable the EntityMovement component
        animatedSprites.enabled = false; // Disable the AnimatedSprites component
        spriteRenderer.sprite = shellSprite; // Change the sprite to the shell sprite
    }

    private void PushShell(Vector2 direction)  // Private method to handle the Koopa's shell being pushed
    {
        isPushed = true; // Set isPushed to true
        GetComponent<Rigidbody2D>().isKinematic = false;  // Disable kinematic mode on the Rigidbody2D component
        entityMovement.direction = direction.normalized; // Set the movement direction to the normalized direction vector
        entityMovement.speed = shellSpeed; // Set the movement speed to shellSpeed
        entityMovement.enabled = true; // Enable the EntityMovement component
        gameObject.layer = LayerMask.NameToLayer("Shell"); // Change the game object's layer to "Shell"
    }

    protected override void OnTriggerEnter2D(Collider2D other)  // Override the OnTriggerEnter2D method from the base Enemy class
    {
        if (isShelled && other.CompareTag("Player")) // Check if the Koopa is in its shell state and the collider is the player
        {
            if (!isPushed) // Check if the shell is not being pushed
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f); // Calculate the direction vector when collided with
                PushShell(direction); // Call the PushShell method with the calculated direction
            }
            else
            {
                Player player = other.GetComponent<Player>();// Get reference to the Player component from the collided objects
                if (player.starPower) // Check if the player has star power
                {
                    Die(); // Call the Die method to destroy the Koopa
                }
                else
                {
                    player.Hit(); // Call the Hit method on the player to deal damage
                }
            }
        }
        else if (!isShelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) // Check if the Koopa is not in its shell state and collides with another shell
        {
            Die();// Call the Die method to destroy the Koopa
        }
        if (other.CompareTag("DeathBarrier")) // Check if the Koopa collides with the death barrier
        {
            Destroy(gameObject); // Destroy the Koopa game object
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) // Override the OnCollisionEnter2D method from the base Enemy class
    {
        if (!isShelled && other.gameObject.CompareTag("Player")) // Check if the Koopa is not in its shell state and collides with the player
        {
            Player player = other.gameObject.GetComponent<Player>(); // Get reference to the Player component
            if (player.starPower) // Check if the player has star power
            {
                Die(); // Call the Die method to destroy the Koopa
            }
            else if (other.transform.DotTest(transform, Vector2.down)) // Check if the player hits the Koopa from above using a dot test
            {
                Hit(); // Call the Hit method to deal damage to the Koopa
            }
            else
            {
                player.Hit(); // Call the Hit method on the player to deal damage
            }
        }

    }
}
