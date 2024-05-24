using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gombaas : Enemy
{
    [SerializeField] private Sprite flatSprite;

    protected override void Hit()
    {
        if (enemyLives > 1)
        {
            SubtractLife();
            DisplayLives();
            Debug.Log("Goomba hit");
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
        Debug.Log("Goomba flattened");
    }
}


