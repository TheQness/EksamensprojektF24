using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Mario").transform;
    }
    // using lateupdate garantues that the camera posotion updates after marios position is updated in fixedupdate
    private void LateUpdate()
    { 
        //gets current position of camera and stores it in a vector 3 called cameraPosition
        Vector3 cameraPosition = transform.position;
        // updates the x-position of the cameras position to match the players x-position, but ensures it never moves left
        //mathf.max returns the larget value of Max(a,b). if player moves left, the cameras holds its position, as it gets returned.
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        //updates the camera position to the updates cameraPosition
        transform.position = cameraPosition;
    }
}
