using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Block
{
    private enum Direction
    {
        DOWN,
        RIGHT,
        UP,
        LEFT,
    };

    [SerializeField] private Vector2 newPosition;
    [SerializeField] private Direction gravityDirection;
    [SerializeField] private Direction currentGravityDirection;

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        base.PlayerEnterAction(playerMovement);

        playerMovement.transform.position = newPosition;
        
        // THE ORDER MATTERS WHICH IS KINDA ASS SMH
        this.HandlePlayerRotation(playerMovement);
        this.HandleGravity(playerMovement);
        this.ResetPlayerVelocity(playerMovement);
    }

    private void HandleGravity(PlayerMovement playerMovement)
    {
        Physics2D.gravity = (playerMovement.transform.rotation * Vector2.down) * Physics2D.gravity.magnitude;
    }

    private void HandlePlayerRotation(PlayerMovement playerMovement)
    {
        playerMovement.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        // Get difference between two quaternions
        Quaternion playerRotation = playerMovement.transform.rotation;
        Quaternion currentGravityDirection = Quaternion.Euler(0, 0, (float) this.currentGravityDirection * 90f);

        Debug.Log("PLAYER ROTATION: " + playerRotation);
        Debug.Log("GRAVITY ROTATION: " + currentGravityDirection);

        Quaternion difference = playerRotation * Quaternion.Inverse(currentGravityDirection);
        // float difference = 0;
        playerMovement.transform.rotation = Quaternion.Euler(0, 0, (float) this.gravityDirection * 90f) * difference;

        // Kinda confusing visually. I dunno if it's a good idea. Maybe make it a setting?
        GameManager.instance.RotateSceneCamera(playerMovement.transform.rotation.eulerAngles.z);
    }

    private void ResetPlayerVelocity(PlayerMovement playerMovement)
    {
        playerMovement.ResetVelocity();
    }
}
