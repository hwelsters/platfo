using System;
using System.Collections;
using UnityEngine;

public class ShakyBlock : Block
{
    [SerializeField] 
    private Sprite activatedSprite;

    private SpriteRenderer spriteRenderer;
    private Coroutine currentCoroutine;

    private bool playerIsTouching = false;
    
    // Repeated everywhere, how should I stop copy and pasting
    private int playerCount = 0;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void PlayerEnterAction(PlayerMovement playerMovement) 
    {
        base.PlayerEnterAction(playerMovement);

        this.playerIsTouching = true;
        this.spriteRenderer.sprite = activatedSprite;

        StartCoroutine(Shake());
    }

    protected override void PlayerExitAction(PlayerMovement playerMovement) 
    {
        base.PlayerExitAction(playerMovement);
        
        if (this.currentCoroutine != null) StopCoroutine(this.currentCoroutine);
        Destroy(gameObject);
    }

    private IEnumerator Shake()
    {
        const float shakeSpeed = 32.5f;
        const float amplitude = 0.025f;

        Vector2 originalPosition = transform.position;
        float radians = 0;
        float displacement = 0;

        while (playerIsTouching)
        {
            radians += Time.deltaTime * shakeSpeed;
            displacement = Mathf.Sin(radians) * amplitude;
            transform.position = originalPosition + new Vector2(displacement, 0);
            yield return null;
        }

        transform.position = originalPosition;
    }
}
