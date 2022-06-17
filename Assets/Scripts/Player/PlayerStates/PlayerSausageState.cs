using UnityEngine;

public class PlayerSausageState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Sausage State!");

        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(2f, 0.75f);
        capsuleCollider2D.offset = new Vector2(0f, -0.125f);
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;

        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this.xVelocity)) animator.Play("Birdsausage_Idle");
    }
}
