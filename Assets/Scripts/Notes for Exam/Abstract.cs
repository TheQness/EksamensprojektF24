using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Abstract : MonoBehaviour
{
    /// <summary>
    /// Like interfaces, abstract classes cannot be used to create objects
    /// Any class that subclasses from an abstract class must fully implement all variables and methods marked with the abstract keyword.
    /// They can be particularly useful in situations where you want to use class inheritance without having to write out a base class’s default implementation
    /// Abstract classes can contain abstract methods, which are declared without any implementation. These methods must be implemented by any non-abstract derived class.
    /// Can contain variables / fields values
    ///Abstract classes can also contain regular methods, properties, fields, and events with implementations.
    /// Abstract classes are often used to define common behavior or structure among a group of related classes.
    /// Abstract classes define a contract for derived classes by specifying the methods they must implement.
    ///  huge efficiency boost to your code base, breaking away from long, messy subclassing hierarchies
    /// </summary>

    protected TMP_Text livesText; // Reference to the TMP_Text component for displaying enemy lives.
    protected AnimatedSprites animatedSprites; // Reference to the AnimatedSprites component for enemy animations.
    protected DeathAnimation deathAnimation; // Reference to the DeathAnimation script on the enemy.
    protected SpriteRenderer spriteRenderer;
    protected EntityMovement entityMovement;
    protected int enemyLives; // Current number of lives for the enemy.
    private Vector3 smallScaleSize = new Vector3(1, 1, 1);// Scale size for small enemies.
    private Vector3 mediumScaleSize = new Vector3(1.5f, 1.5f, 1.5f); // Scale size for medium enemies.
    private Vector3 largeScaleSize = new Vector3(2f, 2f, 2f); // Scale size for large enemies.
    private float smallYOffset = 0f; // Y offset for small enemies.
    protected float mediumYOffset = 0.25f; // Y offset for medium enemies.
    protected float largeYOffset = 0.5f;  // Y offset for large enemies.

    public enum Size // Enum representing the size of the enemy.
    {
        Small,
        Medium, 
        Large
    }
    public Size size; // The size of the enemy, gets set in inspector

    protected abstract void Awake();

    private void Start()
    {
        SetLives(); // Sets the initial number of lives based on the enemy size.
        ScaleSize(); // Scales the enemy size based on the number of lives.
        DisplayLives(); // Updates the display of enemy lives.
    }

    protected abstract void Hit();


    private void DisplayLives()
    {
        livesText.text = enemyLives.ToString(); // Updates the text component of livesText by converting enemyLives to a string
    }

    private void SetLives() // Determines the number of lives based on the enemy size.
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

    private void ScaleSize() //Scales the size and their y-position depending on their lives.
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

    private Vector3 CalculateNewPosition(float yOffset) //returns the new position of the enemy based on the yOffset
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

    protected abstract void OnTriggerEnter2D(Collider2D other);

    protected abstract void OnCollisionEnter2D(Collision2D other);
    
}
