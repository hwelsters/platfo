using UnityEngine;

public class PlayerState
{
    protected virtual float FALL_MULTIPLIER { get { return 3f; }}
    protected virtual float MOVE_SPEED { get { return 2.5f; } }
    protected virtual float JUMP_SPEED { get { return 6f; } }

    protected float xVelocity = 0f;
    protected float yVelocity = 0f;

    protected FacingDirection facingDirection = FacingDirection.RIGHT;

    protected bool isGrounded = false;

    public virtual void HandleEnter(PlayerMovement playerMovement)
    {
        GameManager.OnWin += OnWin;
    }

    public virtual void HandleInput(PlayerMovement playerMovement)
    {
        this.isGrounded = this.IsGrounded(playerMovement);

        Rigidbody2D rigidbody2D = playerMovement.Rigidbody2D;

        this.xVelocity = Input.GetAxisRaw("Horizontal") * this.MOVE_SPEED;
        this.yVelocity = this.isGrounded && PressedJump() ? this.JUMP_SPEED : -Global.ScalarProjection(rigidbody2D.velocity, Physics2D.gravity);

        // FOUND THE PROBLEM
        if (Global.ScalarProjection(rigidbody2D.velocity, Physics2D.gravity) > 0) this.yVelocity += -Physics2D.gravity.magnitude * (FALL_MULTIPLIER - 1) * Time.deltaTime;
        
        Vector2 gravityDirection = Physics2D.gravity.normalized;
        rigidbody2D.velocity = Vector2.Perpendicular(gravityDirection) * this.xVelocity - gravityDirection * this.yVelocity;

        if (this.isGrounded) Debug.Log("Grounded");
        else Debug.Log("Not grounded");
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

    public virtual void OnWin() {}

    protected bool IsGrounded(PlayerMovement playerMovement)
    {
        Debug.Log(Physics2D.gravity.normalized);

        // UPDATED: Boxcast to cast in direction of gravity
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size - new Vector3(0.2f, 0, 0), 0f, Physics2D.gravity.normalized, 0.2f, LayerMask.GetMask("GroundLayer"));

        return raycastHit.collider != null;
    }

    protected bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.C);
    }
}
