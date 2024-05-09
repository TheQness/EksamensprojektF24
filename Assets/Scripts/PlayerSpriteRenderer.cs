using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite turn;
    public AnimatedSprites run;
        
    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate() //physics is updated before changing the sprites
    {
        run.enabled = movement.isRunning;
        if (movement.isJumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.isTurning)
        {
            spriteRenderer.sprite = turn;
        }

        else if (!movement.isRunning)
        {
            spriteRenderer.sprite = idle;
        }

    }
}
