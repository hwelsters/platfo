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

    [SerializeField]
    private Vector2 newPosition;

    [SerializeField]
    private Direction gravityDirection;

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        base.PlayerEnterAction(playerMovement);
        playerMovement.transform.position = newPosition;
        this.HandleGravity(playerMovement);
        this.HandlePlayerRotation(playerMovement);
    }

    private void HandleGravity(PlayerMovement playerMovement)
    {
        switch (this.gravityDirection)
        {
            case Direction.DOWN:
                Physics2D.gravity = Vector2.down * Physics2D.gravity.magnitude;
                break;
            case Direction.UP:
                Physics2D.gravity = Vector2.up * Physics2D.gravity.magnitude;
                break;
            case Direction.LEFT:
                Physics2D.gravity = Vector2.left * Physics2D.gravity.magnitude;
                break;
            case Direction.RIGHT:
                Physics2D.gravity = Vector2.right * Physics2D.gravity.magnitude;
                break;
        }
        Debug.Log(Physics2D.gravity);
    }

    private void HandlePlayerRotation(PlayerMovement playerMovement)
    {
        playerMovement.transform.rotation = Quaternion.Euler(0, 0, (float) this.gravityDirection * 90f);
    }
}
