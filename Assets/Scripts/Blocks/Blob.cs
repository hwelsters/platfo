using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Block
{
    [SerializeField] private GameObject blockToAppear;
    private BoxCollider2D boxCollider2D;

    // Repeated everywhere, how should I stop copy and pasting
    private int playerCount = 0;

    private void Start()
    {
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        this.playerCount++;

        this.ChangeAnimation();
        this.ExpandBoxColliderSize();
    }

    protected override void PlayerExitAction(PlayerMovement playerMovement)
    {
        this.playerCount--;
        if (this.playerCount == 0) this.MakeBlockAppear();
    }

    private void ChangeAnimation()
    {
        GetComponent<Animator>().Play("BlobOutlineFilled");
    }

    private void MakeBlockAppear()
    {
        blockToAppear.SetActive(true);
    }

    private void ExpandBoxColliderSize()
    {
        this.boxCollider2D.size = new Vector2(0.9f, 0.9f);
        this.boxCollider2D.offset = new Vector2(0, 0);
    }
}
