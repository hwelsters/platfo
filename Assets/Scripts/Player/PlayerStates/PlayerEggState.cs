using UnityEngine;

public class PlayerEggState : PlayerState
{
    public override void HandleEnter(PlayerMovement playerMovement)
    {
        Debug.Log("Entered Egg State!");
        
        CapsuleCollider2D capsuleCollider2D = playerMovement.CapsuleCollider2D;
        capsuleCollider2D.size = new Vector2(1f, 1f);
        capsuleCollider2D.offset = new Vector2(0f, 0f);
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
        
        Animator animator = playerMovement.Animator;
        if (Global.FloatIsZero(this.xVelocity)) animator.Play("Birdegg_Idle");
    }
}
