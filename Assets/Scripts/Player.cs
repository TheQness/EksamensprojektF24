using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;

    public bool isBig => bigRenderer.enabled;
    public bool isDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        Debug.Log("2");
        if (isBig){
            Shrink();
            Debug.Log("3");
        } else {
            Debug.Log("4");
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

    
}
