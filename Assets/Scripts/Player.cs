using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    private DeathAnimation deathAnimation;

    public bool isBig => bigRenderer.enabled;
    public bool isDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }
    public void Hit()
    {
        if (isBig)
        {
            Shrink();
        }
        else 
        {
            Death();
        }
    }

    private void Shrink()
    {
        Debug.Log("Shrink");
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathBarrier"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(3f);
        }
    }
    
}
