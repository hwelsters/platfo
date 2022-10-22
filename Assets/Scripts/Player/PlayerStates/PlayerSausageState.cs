using UnityEngine;

public class PlayerSausageState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Sausage State!");

        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(1.875f, 0.75f);
        capsuleCollider2D.offset = new Vector2(0f, -0.125f);
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
        
        // Only has to be done once
        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this._xVelocity)) animator.Play("Birdsausage_Idle");
    }
    
    public override void OnWin(PlayerMovement playerMovement) 
    {
        base.OnWin(playerMovement);

        Animator animator = playerMovement.Animator;
        animator.Play("Birdsausage_Celebrate");
    }
}
