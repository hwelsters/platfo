using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Spike
{
    [SerializeField] private GameObject walkable;

    public void BecomeWalkable()
    {
        walkable.SetActive(true);
        gameObject.SetActive(false);
    }
}
