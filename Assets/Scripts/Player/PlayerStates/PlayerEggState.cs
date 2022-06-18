using UnityEngine;

public class PlayerEggState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Egg State!");
        
        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(0.9f, 0.9f);
        capsuleCollider2D.offset = new Vector2(0f, 0f);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
    }

    public override void HandleAnimation(PlayerMovement playerMovement)
    {
        base.HandleAnimation(playerMovement);

        Animator animator = playerMovement.Animator;
        if (this.facingDirection == FacingDirection.LEFT) animator.Play("Birdegg_Idle_Left");
        else animator.Play("Birdegg_Idle_Right");
    }
}
