using UnityEngine;

public abstract class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = Global.GetScriptFromCollider<PlayerMovement>(other);
            if (playerMovement != null) PlayerTouchAction(playerMovement);
        }
    }

    protected abstract void PlayerTouchAction(PlayerMovement playerMovement);
}
