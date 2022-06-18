using UnityEngine;

public class PlayerSausageState : PlayerState
{
    protected override float MOVE_SPEED { get { return 2f; } }
    protected override float JUMP_SPEED { get { return 3.5f; } }

    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Sausage State!");

        // Modify CapsuleCollider2D values to fit sprite shape
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(2f, 0.75f);
        capsuleCollider2D.offset = new Vector2(0f, -0.125f);
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
        
        // Only has to be done once
        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this.xVelocity)) animator.Play("Birdsausage_Idle");
    }
}
