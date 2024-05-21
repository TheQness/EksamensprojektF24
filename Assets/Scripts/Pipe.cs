using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    private float shakeThreshold = 3.0f;
    private float minShakeInterval = 0.5f;
    private float sqrShakeThreshold;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;


    private void Start()
    {
        sqrShakeThreshold = Mathf.Pow(shakeThreshold, 2);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && connection != null && Input.acceleration.sqrMagnitude >= sqrShakeThreshold)
        {
            //Go down into hole
            StartCoroutine(EnterPipe(other.transform));
        }
        
    }

    private IEnumerator EnterPipe(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPosition, enteredScale);
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;

    }

}
