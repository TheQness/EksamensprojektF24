using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player; // Reference to the player's transform.
    private const float defaultHeight = 6.5f;  // Default height of the camera.
    private const float undergroundHeight = -9.5f; // Height of the camera when the player is underground.

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform; // Find the player GameObject with the tag "Player" and get its transform.
    }

    private void LateUpdate() // using lateupdate garantues that the camera posotion updates after marios position is updated in fixedupdate
    { 
        Vector3 cameraPosition = transform.position; // Get the current position of the camera and store it in a Vector3 called cameraPosition.
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // Update the x-position of the camera to match the player's x-position but ensure it never moves left.
        transform.position = cameraPosition; //updates the camera position to the updated cameraPosition
    }

    public void SetCameraUnderground(bool isUnderground)  // Method to set the camera's position when the player is underground or above ground.
    {
        Vector3 cameraPosition = transform.position; // Get the current position of the camera.
        cameraPosition.y = isUnderground ? undergroundHeight : defaultHeight;  // Set the y-position of the camera to undergroundHeight if isUnderground is true, otherwise set it to the default height.
        transform.position = cameraPosition; // Update the camera position to the updated cameraPosition.
    }
}
