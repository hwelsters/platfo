using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lava")) other.GetComponent<LavaBlock>().BecomeWalkable();
        DestroySelf();
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}
