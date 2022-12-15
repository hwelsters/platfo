using UnityEngine;

public class PlayerEggState : PlayerState
{
    protected override float JUMP_SPEED { get { return 0f; }}

    public override void HandleEnter(PlayerMovement playerMovement)
    {
        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(0.9f, 0.9f);
        capsuleCollider2D.offset = new Vector2(0f, -0.05f);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
    }

    public override void HandleAnimation(PlayerMovement playerMovement)
    {
        base.HandleAnimation(playerMovement);

        Animator animator = playerMovement.Animator;
        if (this._facingDirection == FacingDirection.LEFT) animator.Play("Birdegg_Idle_Left");
        else animator.Play("Birdegg_Idle_Right");
    }

    public override void OnWin(PlayerMovement playerMovement) 
    {
        base.OnWin(playerMovement);
        Animator animator = playerMovement.Animator;

        if (this._facingDirection == FacingDirection.LEFT) animator.Play("Birdegg_Celebrate_Left");
        else animator.Play("Birdegg_Celebrate_Right");
    }

    public override void OnDie(PlayerMovement playerMovement)
    {
        Animator animator = playerMovement.Animator;

        if (this._facingDirection == FacingDirection.LEFT) animator.Play("Birdegg_Dying_Left");
        else animator.Play("Birdegg_Dying_Right");
    }
}
