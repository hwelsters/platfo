using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggship : Block
{
    private const float MOVE_SPEED = 0.5f;

    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb2D;
    // VERY TEMPORARY FIX
    private Rigidbody2D otherRb2D;
    private Vector2 lastPosition;

    private Vector2 currVelocity { get { return ((Vector2) transform.position - this.lastPosition) / Time.deltaTime; }} 

    private void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
    }

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        this.otherRb2D = playerMovement.GetComponent<Rigidbody2D>();
        this.direction *= -1f;
        transform.localScale = new Vector2(this.direction.x, 1f);
    }

    protected override void PlayerExitAction(PlayerMovement playerMovement)
    {
        this.otherRb2D = null;
    }

    private void FixedUpdate()
    {
        if (this.rb2D != null) this.rb2D.velocity = this.direction * MOVE_SPEED;

        if (this.otherRb2D != null) this.otherRb2D.velocity = this.currVelocity + this.otherRb2D.velocity;
        this.lastPosition = transform.position;
    }
}
