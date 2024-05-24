using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    protected TMP_Text livesText; // Reference to the TMP_Text component for displaying enemy lives.
    protected AnimatedSprites animatedSprites; // Reference to the AnimatedSprites component for enemy animations.
    protected DeathAnimation deathAnimation; // Reference to the DeathAnimation script on the enemy.
    protected SpriteRenderer spriteRenderer;
    protected EntityMovement entityMovement;
    protected int enemyLives; // Current number of lives for the enemy.
    protected Vector3 smallScaleSize = new Vector3(1, 1, 1);// Scale size for small enemies.
    protected Vector3 mediumScaleSize = new Vector3(1.5f, 1.5f, 1.5f); // Scale size for medium enemies.
    protected Vector3 largeScaleSize = new Vector3(2f, 2f, 2f); // Scale size for large enemies.
    protected float smallYOffset = 0f; // Y offset for small enemies.
    protected float mediumYOffset = 0.25f; // Y offset for medium enemies.
    protected float largeYOffset = 0.5f;  // Y offset for large enemies.

    public enum Size // Enum representing the size of the enemy.
    {
        Small,
        Medium, 
        Large
    }
    public Size size; // The size of the enemy, gets set in inspector

    protected virtual void Awake()
    {
        animatedSprites = GetComponent<AnimatedSprites>(); // Retrieves the AnimatedSprites component.
        deathAnimation =  GetComponent<DeathAnimation>(); // Retrieves the DeathAnimation component.
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityMovement = GetComponent<EntityMovement>();
        livesText = GetComponentInChildren<TMP_Text>(); // Retrieves the TMP_Text component from child objects.
    }

    protected void Start()
    {
        SetLives(); // Sets the initial number of lives based on the enemy size.
        ScaleSize(); // Scales the enemy size based on the number of lives.
        DisplayLives(); // Updates the display of enemy lives.
    }

    protected virtual void Hit()
    {
        if (enemyLives >= 2) // If enemy has 2 lives or more, Subtracts a enemylife and displays the lives
        {
            SubtractLife();
        }
        else if (enemyLives == 1) //If enemy has 1 life, it dies and initiates the death sequence. when hit
        {
            Die();
        }
    }

    protected void DisplayLives()
    {
        livesText.text = enemyLives.ToString(); // Updates the text component of livesText by converting enemyLives to a string
    }

    protected void SetLives() // Determines the number of lives based on the enemy size.
    {
        switch (size)
        {
            case Size.Small:
            {
                enemyLives = 2;
                break;
            }
            case Size.Medium:
            {
                enemyLives = 4;
                break;
            }
            case Size.Large:
            {
                enemyLives = 6;
                break;
            }
            default:
            {
                Debug.LogError("Size not found");
                break;
            }

        }
    }

    protected void ScaleSize() //Scales the size and their y-position depending on their lives.
    {
        if (enemyLives >= 0 && enemyLives <= 2)
        {   
            transform.position = CalculateNewPosition(smallYOffset); // Adjusts the position of the enemy based on its size.
            transform.localScale = smallScaleSize; // Sets the scale of the enemy based on its size.

        }
        else if (enemyLives >= 3 && enemyLives <= 4)
        {
            transform.position = CalculateNewPosition(mediumYOffset);
            transform.localScale = mediumScaleSize;
            
        }
        else if (enemyLives >= 5)
        {
            transform.position = CalculateNewPosition(largeYOffset);
            transform.localScale = largeScaleSize;
        }
    }

    protected Vector3 CalculateNewPosition(float yOffset) //returns the new position of the enemy based on the yOffset
    {
        Vector3 newPosition = transform.position; //newPosition is set to the current position of the enemy
        newPosition.y += yOffset; //adds the yOffset given as a parameter to the y-axis in the newPosition
        return newPosition; //returns new position

    }

    protected void SubtractLife()
    {
        enemyLives--; // Decrements enemylives by 1
        DisplayLives();
        if (enemyLives == 4 || enemyLives == 2) //Ensures it only gets called when the size changes 
        {
            ScaleSize(); //Scales size
        }
    }

    protected void Die()
    {
        animatedSprites.enabled = false; // Disables walk animation.
        deathAnimation.enabled = true; //Enables death animation
        Destroy(gameObject, 3f); // Destroys the enemy object after a delay of 3 seconds
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) //Checks layer of trigger collider
        {
            Die(); // Initiates death when hit by a shell.
        }
        if (other.CompareTag("DeathBarrier"))
        {
            Destroy(gameObject); // Destroys the enemy when it hits a death barrier.
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>(); // Retrieves the Player component from the collided object.
            if (player.starPower)
            {
                Die(); // Initiates death if the player has star power.
            }
            else if (other.transform.DotTest(transform, Vector2.down)) //performs DotTest to if collision happenedes from above.
            {
                Hit(); //Initiates hit if the player stomped on the enemy
            }
            else
            {
                player.Hit(); //Player gets hit if nothing else is true
            }
        }
    }
}
