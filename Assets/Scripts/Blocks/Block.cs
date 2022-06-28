using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement playerMovement = Global.GetScriptFromCollider<PlayerMovement>(other);
        if (playerMovement != null) PlayerEnterAction(playerMovement);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerMovement playerMovement = Global.GetScriptFromCollider<PlayerMovement>(other);
        if (playerMovement != null) PlayerExitAction(playerMovement);
    }

    protected virtual void PlayerEnterAction(PlayerMovement playerMovement)
    {
        Debug.Log("Player entered block");
    }
    protected virtual void PlayerExitAction(PlayerMovement playerMovement)
    {
        Debug.Log("Player exited block");
    }
}
