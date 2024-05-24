using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions //does not inheret from MonoBehavior
{
    private const string defaultLayerMask = "Default"; //Constant string representing the default layer name. safety to ensure correct layermask is ignored.
    private static LayerMask layerMask = LayerMask.GetMask(defaultLayerMask); // Default layer mask retrieved using the default layer name..

    public static bool Raycast(this Rigidbody2D rigidBody, Vector2 direction, float distance = 0.375f)  // Extension method to perform a circle raycast from a Rigidbody2D. Returns a bool
    {
        if (rigidBody.isKinematic) // Check if the Rigidbody2D is kinematic (not controlled by physics).
        {
            return false; // If kinematic, return false as the raycast cannot be performed.
        }

        float radius = 0.25f; // Radius of the circle used for the circle raycast.

    RaycastHit2D hit = Physics2D.CircleCast(rigidBody.position, radius, direction.normalized, distance, layerMask); // Perform the circle raycast and store the result in a RaycastHit2D variable named hit.
    return hit.collider != null ;  // Return true if the raycast hits a collider and the hit object's rigidbody is different from the caller's rigidbody.
   }

    /// <summary>
    /// Determines if Mario hits an enemy from above or a block from below by calculating the dot product between vectors. Calculates the angle between 2 vectors, 
    /// </summary>
    /// <param name="transform">The transform of Mario.</param>
    /// <param name="other">The transform of the collided object.</param>
    /// <param name="testDirection">The direction vector, which mario's should be close to to return true, fx down when jumping ontop an enemy".</param>
    /// <returns>True if the angle between vectors is acute, indicating a hit from above; otherwise, false.</returns>

   public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) //extension to the Transform class to perform a dot product test between two transforms. Determine if Maria
   {
        float detectionThreshold = 0.1f; // Threshold of angle used to detect if an object is above another.
        Vector2 direction = other.position - transform.position; // Calculate the direction vector that points from 'transform' to 'other'. 
        return Vector2.Dot(direction.normalized, testDirection) > detectionThreshold;  // Calculate the dot product between the normalized direction vector and the test direction.
   }

    /// <summary>
    /// normalized makes the range of the dot product go between -1 and 1. 
    /// If the dot product is 1, it means the vectors are pointing in exactly the same direction.
    /// If the dot product is -1, it means the vectors are pointing in exactly opposite directions.
    /// If the dot product is 0, it means the vectors are orthogonal (perpendicular) to each other.
    /// </summary>
}
