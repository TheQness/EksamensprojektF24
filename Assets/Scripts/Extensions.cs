using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions //does not inheret from MonoBehavior
{
    private const string defaultLayerMask = "Default"; //safety to ensure correct layermask is ignored. constant to not be changed during runtime.
    private static LayerMask layerMask = LayerMask.GetMask(defaultLayerMask); //retrieves the layermask Default.

    public static bool Raycast(this Rigidbody2D rigidBody, Vector2 direction) //returns a bool. defines an extension method named Raycast for objects of type Rigidbody2D
    {
        if (rigidBody.isKinematic) //checking if the physics engine is not controlling the object, if not return false
        {
            return false;
        }

        float radius = 0.25f; //collider radius for circle raycast
        float distance = 0.375f;

    RaycastHit2D hit = Physics2D.CircleCast(rigidBody.position, radius, direction.normalized, distance, layerMask); //casts a circle-shaped ray in the specified direction from the position of the rigidBody, which is stored in a RaycastHit2D variable named hit.
    return hit.collider != null ; //returns true if the raycast hits a collider and the rigidbody of the object hit is not the same as the rigidbody on which the method is called. If either condition is not met, it returns false.
   }

   public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) //extension to the Transform class, calculates the dot product (angle) between the direction vector from Mario to the collided object
   {
    float detectionThreshold = 0.1f; //threshold that detects, if the object is above him. if the angle is 
    Vector2 direction = other.position - transform.position; //gives a vector that points from marios position to the collided objects direction. 
    return Vector2.Dot(direction.normalized, testDirection) > detectionThreshold; //normalized to set the length of vector to 1, as we are bot interested in the length, only the direction. measures how much the two vector points in the same direction
   // normalized makes the range of the dot product go between -1 and 1. 
    //If the dot product is 1, it means the vectors are pointing in exactly the same direction.
    //If the dot product is -1, it means the vectors are pointing in exactly opposite directions.
    //If the dot product is 0, it means the vectors are orthogonal (perpendicular) to each other.
   //If the dot product result (which represents the cosine of the angle between the two vectors) is greater than the detection threshold , it indicates that the angle between the vectors is acute, meaning Mario is hitting the object above him.
   }
}
