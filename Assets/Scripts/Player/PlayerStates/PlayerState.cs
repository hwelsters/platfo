using UnityEngine;

public abstract class PlayerState
{
    protected virtual float MOVE_SPEED { get { return 2.5f; } }
    protected virtual float JUMP_SPEED { get { return 5f; } }

    protected float xVelocity = 0f;
    protected float yVelocity = 0f;

    protected FacingDirection facingDirection = FacingDirection.RIGHT;

    public abstract void HandleEnter(PlayerMovement playerMovement);

    public virtual void HandleInput(PlayerMovement playerMovement)
    {
        Rigidbody2D rigidbody2D = playerMovement.Rigidbody2D;

        this.xVelocity = Input.GetAxisRaw("Horizontal") * this.MOVE_SPEED;
        this.yVelocity = this.IsGrounded(playerMovement) && PressedJump() ? this.JUMP_SPEED : rigidbody2D.velocity.y;

        if (rigidbody2D.velocity.y < 0) this.yVelocity *= 1.01f;
        rigidbody2D.velocity = new Vector2(this.xVelocity, this.yVelocity);
    }

    public virtual void HandleAnimation(PlayerMovement playerMovement)
    {
        if (!Global.FloatIsZero(this.xVelocity))
        {
            playerMovement.transform.localScale = new Vector2(Mathf.Sign(this.xVelocity), 1);

            if (this.xVelocity < 0) this.facingDirection = FacingDirection.LEFT;
            else this.facingDirection = FacingDirection.RIGHT;
        }
    }

    protected bool IsGrounded(PlayerMovement playerMovement)
    {
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0f, Vector2.down, 0.25f, LayerMask.GetMask("GroundLayer"));

        return raycastHit.collider != null;
    }

    protected bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.C);
    }
}
