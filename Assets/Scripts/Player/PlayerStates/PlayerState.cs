using UnityEngine;

public class PlayerState
{
    protected virtual float FALL_MULTIPLIER { get { return 1.5f; }}
    protected virtual float MOVE_SPEED { get { return 2.5f; } }
    protected virtual float JUMP_SPEED { get { return 5.75f; } }

    protected float _xVelocity = 0f;
    protected float _yVelocity = 0f;

    protected FacingDirection _facingDirection = FacingDirection.RIGHT;

    protected bool _isGrounded = false;

    public virtual void HandleEnter(PlayerMovement playerMovement) {}

    public virtual void HandleInput(PlayerMovement playerMovement)
    {
        this._isGrounded = this.IsGrounded(playerMovement);

        Rigidbody2D rigidbody2D = playerMovement.Rigidbody2D;

        this._xVelocity = Input.GetAxisRaw("Horizontal") * this.MOVE_SPEED;
        this._yVelocity = this._isGrounded && PressedJump() ? this.JUMP_SPEED : -Global.ScalarProjection(rigidbody2D.velocity, Physics2D.gravity);

        // FOUND THE PROBLEM
        // if (Global.ScalarProjection(rigidbody2D.velocity, Physics2D.gravity) > 0) this._yVelocity += -Physics2D.gravity.magnitude * (FALL_MULTIPLIER - 1) * Time.deltaTime;
        
        Vector2 gravityDirection = Physics2D.gravity.normalized;
        rigidbody2D.velocity = Vector2.Perpendicular(gravityDirection) * this._xVelocity - gravityDirection * this._yVelocity;
    }

    public virtual void HandleAnimation(PlayerMovement playerMovement)
    {
        if (!Global.FloatIsZero(this._xVelocity))
        {
            playerMovement.transform.localScale = new Vector2(Mathf.Sign(this._xVelocity), 1);

            if (this._xVelocity < 0) this._facingDirection = FacingDirection.LEFT;
            else this._facingDirection = FacingDirection.RIGHT;
        }
    }

    public virtual void OnWin(PlayerMovement playerMovement) 
    {
        Rigidbody2D rigidbody2D = playerMovement.Rigidbody2D;
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        Debug.Log("PLAYER WON!"); 
    }

    protected bool IsGrounded(PlayerMovement playerMovement)
    {
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
