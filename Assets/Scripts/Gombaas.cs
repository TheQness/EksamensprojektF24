using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gombaas : Enemy
{
    [SerializeField] private Sprite flatSprite;

    protected override void Hit()
    {
        if (enemyLives >= 2)
        {
            SubtractLife();
            DisplayLives();
        }
        else if (enemyLives == 1)
        {
            Flatten();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
}


