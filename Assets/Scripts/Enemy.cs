using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    protected TMP_Text livesText;
    protected AnimatedSprites animatedSprites;
    protected DeathAnimation deathAnimation;
    protected int enemyLives;
    protected Vector3 smallScaleSize = new Vector3(1, 1, 1);
    protected Vector3 mediumScaleSize = new Vector3(1.5f, 1.5f, 1.5f);
    protected Vector3 largeScaleSize = new Vector3(2f, 2f, 2f);
    protected float smallYPosition = 0f;
    protected float mediumYPosition = 0.25f;
    protected float largeYPosition = 0.5f;

    public enum Size
    {
        Small,
        Medium, 
        Large
    }
    public Size size;

    protected virtual void Awake()
    {
        animatedSprites = GetComponent<AnimatedSprites>();
        deathAnimation =  GetComponent<DeathAnimation>();
        livesText = GetComponentInChildren<TMP_Text>();
    }

    protected void Start()
    {
        SetLives();
        ScaleSize();
        DisplayLives();
    }

    protected virtual void Hit()
    {
        if (enemyLives >= 2)
        {
            SubtractLife();
            DisplayLives();
        }
        else if (enemyLives == 1)
        {
            Die();
        }
    }

    protected void DisplayLives()
    {
        livesText.text = enemyLives.ToString();
    }

    protected void SetLives()
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

    protected void ScaleSize()
    {
        if (enemyLives >= 0 && enemyLives <= 2)
        {   
            transform.position = CalculateNewPosition(smallYPosition);
            Debug.Log(transform.position);
            transform.localScale = smallScaleSize;

        }
        else if (enemyLives >= 3 && enemyLives <= 4)
        {
            transform.position = CalculateNewPosition(mediumYPosition);
            Debug.Log(transform.position);
            transform.localScale = mediumScaleSize;
            
        }
        else if (enemyLives >= 5)
        {
            transform.position = CalculateNewPosition(largeYPosition);
            Debug.Log(transform.position);
            transform.localScale = largeScaleSize;
        }
    }

    protected Vector3 CalculateNewPosition(float yPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.y += yPosition;
        return newPosition;

    }

    protected void SubtractLife()
    {
        enemyLives--;
        ScaleSize();
    }

    protected void Die()
    {
        animatedSprites.enabled = false; //stops walk animation
        deathAnimation.enabled = true; //starts death animation
        Destroy(gameObject, 3f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Die();
        }
        if (other.CompareTag("DeathBarrier"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.starPower)
            {
                Die();
            }
            else if (other.transform.DotTest(transform, Vector2.down))
            {
                Hit();
            }
            else
            {
                player.Hit();
            }
        }
    }
}
