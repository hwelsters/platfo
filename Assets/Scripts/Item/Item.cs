using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item<T> : MonoBehaviour
    where T:PlayerState, new()
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.ChangePlayerState(other);
            this.DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void ChangePlayerState(Collider2D other)
    {
        PlayerMovement playerMovement = Global.GetScriptFromCollider<PlayerMovement>(other);
        playerMovement.PlayerState = new T();
    }

}