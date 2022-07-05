using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OffsetAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();

        float randomOffset = Random.Range(0.0f, 1.0f);
        this.animator.SetFloat("Offset", randomOffset);
    }
}
