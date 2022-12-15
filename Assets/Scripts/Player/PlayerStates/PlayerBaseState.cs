using UnityEngine;

public class PlayerBaseState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(0.75f, 0.85f);
        capsuleCollider2D.offset = new Vector2(0f, -0.075f);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
    }

    public override void HandleAnimation(PlayerMovement playerMovement)
    {
        base.HandleAnimation(playerMovement);

        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this._xVelocity))
        {
            animator.Play("Birdman_Idle");
        }
        else 
        {
            animator.Play("Birdman_Run");
        }
    }
    
    public override void OnWin(PlayerMovement playerMovement) 
    {
        base.OnWin(playerMovement);
        Animator animator = playerMovement.Animator;

        bool isAlreadyCelebrating = animator.GetCurrentAnimatorStateInfo(0).IsName("Birdman_Celebrate_Loop");
        if (!isAlreadyCelebrating) animator.Play("Birdman_Celebrate");
    }

    
    public override void OnDie(PlayerMovement playerMovement)
    {
        Animator animator = playerMovement.Animator;
        animator.Play("Birdman_Dying");
    }
}
