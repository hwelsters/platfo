using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Block
{
    [SerializeField]
    private Vector2 newPosition;

    protected override void PlayerEnterAction(PlayerMovement playerMovement) 
    {
        Debug.Log(Physics2D.gravity);
        base.PlayerEnterAction(playerMovement);
        playerMovement.transform.position = newPosition;
    }
}
