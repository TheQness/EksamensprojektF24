using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite deathSprite;
    public SpriteRenderer spriteRenderer;
    private float elapsed = 0f;
    private float animationDuration = 3f;
    private float upwardsVelocity = 10f;
    private float gravity = -36f;


    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; //makes sure that when the character dies, the character is shown falling
        if (deathSprite !=null)
        {
            spriteRenderer.sprite = deathSprite;
        }
      
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        Vector3 velocity = Vector3.up * upwardsVelocity;
        while (elapsed < animationDuration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
