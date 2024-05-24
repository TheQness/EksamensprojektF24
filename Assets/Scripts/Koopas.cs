using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Koopas : Enemy
{
    [SerializeField] private Sprite shellSprite;
    public bool isShelled;
    public bool isPushed;
    private float shellSpeed = 12f;

    protected virtual void Awake()
    {
        animatedSprites = GetComponent<AnimatedSprites>();
        deathAnimation =  GetComponent<DeathAnimation>();
        livesText = GetComponentInChildren<TMP_Text>();
        mediumYPosition = 0.38f;
        largeYPosition = 0.75f;
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
            EnterShell();
        }
    }

    private void EnterShell()
    {
        isShelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        isPushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isShelled && other.CompareTag("Player"))
        {
            if (!isPushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                if (player.starPower)
                {
                    Die();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else if (!isShelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
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
        if (!isShelled && other.gameObject.CompareTag("Player"))
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
