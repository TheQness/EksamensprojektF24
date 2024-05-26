using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer; // Reference to the sprite renderer for the small Mario.
    public PlayerSpriteRenderer bigRenderer;  // Reference to the sprite renderer for the big Mario.
    private PlayerSpriteRenderer activeRenderer;  // Reference to the currently active sprite renderer.

    private DeathAnimation deathAnimation; // Reference to the death animation component.
    private CapsuleCollider2D capsuleCollider; // Reference to the capsule collider component.

    public bool isBig => bigRenderer.enabled; // Property to check if the player is big.
    public bool isDead => deathAnimation.enabled; // Property to check if the player is dead.
    public bool starPower {get; private set; } // Bool to check if the player has star power.

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>(); // Assign DeathAnimation component reference.
        capsuleCollider = GetComponent<CapsuleCollider2D>(); // Assign CapsuleCollider2D component reference.
        activeRenderer = smallRenderer;// Set active renderer to smallRenderer initially.
    }
    public void Hit() // Handle player getting hit.
    {
        if (!isDead && !starPower) // Check if the player is not dead and not under star power.
        {
            if (isBig) // Check if the player is big.
            {
                Shrink();// Shrink the player.
            }
            else 
            {
                Death();  // Otherwise, the player dies.
            }
        }
        
    }

    private void Death()// Handle player's death.
    {
        smallRenderer.enabled = false; // Disable the small renderer.
        bigRenderer.enabled = false; // Disable the big renderer.
        deathAnimation.enabled = true; // Enable the death animation.
        GameManager.Instance.ResetLevel(2f); // Reset the level after a delay.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathBarrier")) // Check if the player collides with a death barrier.
        {
            gameObject.SetActive(false); // Disable the player game object.
            GameManager.Instance.ResetLevel(2f); // Reset the level after 2 second delay
        }
    }

    private void Shrink() // Shrink the player.
    {
        smallRenderer.enabled = true; // Enable the small renderer.
        bigRenderer.enabled = false; // Disable the big renderer.
        activeRenderer = smallRenderer; // Set active renderer to smallRenderer.

        capsuleCollider.size = new Vector2(1f, 1f); // Adjust the collider size for small Mario.
        capsuleCollider.offset = new Vector2(0f, 0f); // Adjust the collider offset for small Mario
        
        StartCoroutine(ScaleAnimation());  // Start the scaling animation.
    }
    

    public void Grow()
    {
        smallRenderer.enabled = false; // Disable the small renderer.
        bigRenderer.enabled = true; // Enable the big renderer.
        activeRenderer = bigRenderer; // Set active renderer to bigRenderer.

        capsuleCollider.size = new Vector2(1f, 2f); // Adjust the collider size for big Mario.
        capsuleCollider.offset = new Vector2(0f, 0.5f); // Adjust the collider offset for big Mario.

        StartCoroutine(ScaleAnimation()); // Start the scaling animation.
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f; // Time elapsed since the start of the movement.
        float duration = 0.5f;// Duration of the movement.

        while (elapsed < duration)//while loop, that loops while the elapsed time is less than the duration of the animation
        {
            if (Time.frameCount % 4 == 0) //modulos 4, toggles renderes every 4 frame
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            elapsed += Time.deltaTime; // Increment elapsed time.

            yield return null; // Wait for the next frame
        }

        //enables the active sprite renderer
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void StarPower()
    {
        StartCoroutine(StartPowerAnimation());
    }

    // Activate star power.
    private IEnumerator StartPowerAnimation()
    {
        starPower = true; // Activate star power.
        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            if(Time.frameCount % 4 == 0)//modulos 4, changes color of sprite  every 4 frame
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        activeRenderer.spriteRenderer.color = Color.white; //sets color back to "normal"
        starPower = false; //Disables starpower
    }
}
