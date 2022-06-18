using UnityEngine;

public class PlayerBaseState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Base State!");
        
        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(0.75f, 1f);
        capsuleCollider2D.offset = new Vector2(0f, 0f);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
    }

    public override void HandleAnimation(PlayerMovement playerMovement)
    {
        base.HandleAnimation(playerMovement);

        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this.xVelocity))
        {
            animator.Play("Birdman_Idle");
        }
        else 
        {
            animator.Play("Birdman_Run");
        }
    }
}
