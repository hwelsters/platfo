using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Block
{
    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        playerMovement.Die();
    }
}
