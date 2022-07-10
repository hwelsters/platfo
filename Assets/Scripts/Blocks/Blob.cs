using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Block
{
    [SerializeField]
    private GameObject blockToAppear;

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        this.ChangeAnimation();
    }

    protected override void PlayerExitAction(PlayerMovement playerMovement)
    {
        base.PlayerExitAction(playerMovement);
        this.MakeBlockAppear();
    }

    private void ChangeAnimation()
    {
        GetComponent<Animator>().Play("BlobOutlineFilled");
    }

    private void MakeBlockAppear()
    {
        blockToAppear.SetActive(true);
    }
}
