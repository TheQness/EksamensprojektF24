using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions //does not inheret from MonoBehavior
{
    private const string defaultLayerMask = "Default"; //safety to ensure correct layermask is ignored. constant to not be changed during runtime.
    private static LayerMask layerMask = LayerMask.GetMask(defaultLayerMask); //retrieves the layermask Default.

    public static bool Raycast(this Rigidbody2D rigidBody, Vector2 direction, float radius, float distance) //returns a bool. defines an extension method named Raycast for objects of type Rigidbody2D
    {
        if (rigidBody.isKinematic) //checking if the physics engine is not controlling the object, if not return false
        {
            return false;
        }

    RaycastHit2D hit = Physics2D.CircleCast(rigidBody.position, radius, direction, distance, layerMask); //casts a circle-shaped ray in the specified direction from the position of the rigidBody, which is stored in a RaycastHit2D variable named hit.
    return hit.collider != null ; //returns true if the raycast hits a collider and the rigidbody of the object hit is not the same as the rigidbody on which the method is called. If either condition is not met, it returns false.
   }
}
