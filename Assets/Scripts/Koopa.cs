using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public bool isShelled;
    public bool isPushed;
    private float shellSpeed = 12f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                player.Hit();
            }
        }
        else if (!isShelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
        if (other.CompareTag("DeathBarrier"))
        {
            Destroy(gameObject);
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

    private void Hit()
    {
        GetComponent<AnimatedSprites>().enabled = false; //stops walk animation
        GetComponent<DeathAnimation>().enabled = true; //starts death animation
        Destroy(gameObject, 3f);
    }
}
