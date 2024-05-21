using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float shakeThreshold = 2.0f;
    private float minShakeInterval = 0.5f;
    private float sqrShakeThreshold;
    private float timeSinceLastShake;

    private void Start()
    {
        sqrShakeThreshold = Mathf.Pow(shakeThreshold, 2);
    }

    private void OntriggerStay2D(Collider2D other)
    {
        Debug.Log("Goodbye");
        if (other.CompareTag("Player") && Input.acceleration.sqrMagnitude >= sqrShakeThreshold);
        {
            Debug.Log("Hello");
        }
        
    }
}
